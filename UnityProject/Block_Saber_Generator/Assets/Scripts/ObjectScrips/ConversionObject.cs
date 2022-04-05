using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Minecraft;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Object that handles user interation and async conversion
/// for Beat Saber -> Minecraft packs
/// </summary>
public class ConversionObject : MonoBehaviour
{
    [Header("Progress Bar Colors")]
    [SerializeField] private Color _finishSuccess;
    [SerializeField] private Color _finishFail;

    [Header("Connected Objects")]
    [SerializeField] private TMP_Text title = null;
    [SerializeField] private TMP_Text _status = null;
    [SerializeField] private Slider _progressBar = null;
    [SerializeField] private Image _progressFill = null;
    [SerializeField] private Image _trash = null;

    [Header("Toggle Images")]
    [SerializeField] private Sprite _trashOpen = null;
    [SerializeField] private Sprite _trashClosed = null;


    private string inputPath = "";
    private string outputPath = "";
    private string fileName = "";
    private CancellationTokenSource _asyncSourceCancel = null;


    public string InputPath { get => inputPath; set => inputPath = value; }
    public string OutputPath { get => outputPath; set => outputPath = value; }
    public string FileName { get => fileName; set => fileName = value; }


    public delegate void ObjectEvent(ConversionObject conversionObject);
    public event ObjectEvent OnObjectFinished;
    public event ObjectEvent OnObjectDeleted;


    /// <summary>
    /// Sets up the ConversionObject object ready for conversion
    /// </summary>
    /// <param name="inputPath">file path of the zip file to convert</param>
    /// <param name="outputFolder">folder path to output the two minecraft zip files</param>
    public void Setup(string inputPath, string outputFolder)
    {
        InputPath = inputPath;
        OutputPath = outputFolder;
        FileName = Path.GetFileNameWithoutExtension(inputPath);

        title.text = FileName;
        _status.text = "Waiting...";
        _progressBar.value = 0;
    }

    /// <summary>
    /// Convert Beat saber data to minecraft packs
    /// </summary>
    /// <returns>Async task</returns>
    public async Task ConvertAsync()
    {
        Converting();
        _asyncSourceCancel = new CancellationTokenSource();
        int uuid = Random.Range(-99999999, 99999999);

        int errorCode = await ConvertZip.ConvertAsync(InputPath, OutputPath, uuid, _asyncSourceCancel.Token);
        if (errorCode > 0)
        {
            Failed(errorCode);
        }
        else
        {
            Finished();
        }
        _asyncSourceCancel.Dispose();
        _asyncSourceCancel = null;
        OnObjectFinished?.Invoke(this);
    }


    /// <summary>
    /// Delete and cancel the convertion object
    /// </summary>
    public void DeleteSelf()
    {
        if (_asyncSourceCancel != null)
        {
            _asyncSourceCancel?.Cancel();
        }
        OnObjectDeleted?.Invoke(this);
        Destroy(gameObject);
    }

    public void TrashOpen()
    {
        _trash.sprite = _trashOpen;
    }

    public void TrashClosed()
    {
        _trash.sprite = _trashClosed;
    }

    private void Converting()
    {
        _progressBar.value = _progressBar.maxValue;
        _status.text = "Converting...";
    }

    private void Finished()
    {
        _progressBar.value = _progressBar.maxValue;
        _progressFill.color = _finishSuccess;
        _status.text = "Done!";
    }

    /// <summary>
    /// Set the conversion object fail status
    /// </summary>
    /// <param name="errorCode">Code that determines why the error</param>
    private void Failed(int errorCode = 0)
    {
        _progressBar.value = _progressBar.maxValue;
        _progressFill.color = _finishFail;
        if (errorCode == 1)
        {
            _status.text = "Failed: missing info.dat";
        }
        else if (errorCode == 2 || errorCode == 3)
        {
            _status.text = "Failed: missing song data";
        }
        else if (errorCode == 4)
        {
            _status.text = "Failed: missing song file";
        }
        else
        {
            _status.text = "Failed!";
        }
    }
}
