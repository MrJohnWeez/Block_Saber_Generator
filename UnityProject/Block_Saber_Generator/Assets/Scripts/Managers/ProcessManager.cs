using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using MJW.Conversion;
using UnityEngine;

/// <summary>
/// Handles the async conversion of up to 3 files at a time
/// </summary>
public class ProcessManager : MonoBehaviour
{
    public static string temporaryPath = "";
    public static string streamingAssets = "";


    [SerializeField] private SelectionManager _selectionManager = null;
    [SerializeField] private GameObject _conversionPrefab = null;
    [SerializeField] private GameObject _processingRoot = null;


    private char _driveLetter;


    private List<ConversionObject> _waitingConvert = new List<ConversionObject>();
    private List<ConversionObject> _currentConvert = new List<ConversionObject>();
    private List<ConversionObject> _finishedConvert = new List<ConversionObject>();


    private void Start()
    {
        temporaryPath = GetTempPath();
        streamingAssets = Path.Combine(Application.dataPath, "StreamingAssets");
    }

    private void OnApplicationQuit()
    {
#if UNITY_STANDALONE_WIN
        try
        {
            DriveMapper.UnmapDrive(_driveLetter);
        }
        catch
        {

        }
#endif
    }

    private async void Update()
    {
        if (_currentConvert.Count < 3 && _waitingConvert.Count > 0)
        {
            ConversionObject nextToConvert = _waitingConvert[0];
            _waitingConvert.RemoveAt(0);
            _currentConvert.Add(nextToConvert);
            await nextToConvert.ConvertAsync();
        }
    }


    /// <summary>
    /// Add file for conversion
    /// </summary>
    /// <param name="filePath">Path of the Beat Saber zip file</param>
    public void AddFile(string filePath)
    {
        GameObject newConversion = Instantiate(_conversionPrefab, _processingRoot.transform);
        ConversionObject conversionManager = newConversion.GetComponent<ConversionObject>();
        if (conversionManager)
        {
            conversionManager.ObjectFinished += ConversionFinished;
            conversionManager.ObjectDeleted += ConversionDeleted;
            _waitingConvert.Add(conversionManager);
            conversionManager.Setup(filePath, _selectionManager.OutputPath);
        }
    }


    private void ConversionFinished(ConversionObject conversionObject)
    {
        _currentConvert.Remove(conversionObject);
        _finishedConvert.Add(conversionObject);
    }

    private void ConversionDeleted(ConversionObject conversionObject)
    {
        RemoveConversion(conversionObject);
        Debug.Log("Deleted item with path: " + conversionObject.InputPath);
    }

    private void RemoveConversion(ConversionObject conversionObject)
    {
        _waitingConvert.Remove(conversionObject);
        _currentConvert.Remove(conversionObject);
        _finishedConvert.Remove(conversionObject);
    }

    private string GetTempPath()
    {
#if UNITY_STANDALONE_WIN
        try
        {
            _driveLetter = DriveMapper.SafeMapDrive(Application.temporaryCachePath);
            return $"{_driveLetter}:";
        }
        catch (Win32Exception e)
        {
            Debug.Log("Mapping failed: " + e.Message);
            Debug.Log("Using long temp path. Long song names may not convert correctly due to path size.");
            return Application.temporaryCachePath;
        }
#else
        return Application.temporaryCachePath;
#endif
    }
}
