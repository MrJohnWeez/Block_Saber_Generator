// Created by MrJohnWeez
// June 2020

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProcessManager : MonoBehaviour
{
	public static string temporaryPath = "";
	public static string streamingAssets = "";

	[SerializeField] private SelectionManager _selectionManager = null;
	[SerializeField] private GameObject _conversionPrefab = null;
	[SerializeField] private GameObject _processingRoot = null;
	private List<ConversionObject> _waitingConvert = new List<ConversionObject>();
	private List<ConversionObject> _currentConvert = new List<ConversionObject>();
	private List<ConversionObject> _finishedConvert = new List<ConversionObject>();
	

	private void Start()
	{
		temporaryPath = Application.temporaryCachePath;
		streamingAssets = Path.Combine(Application.dataPath, "StreamingAssets");
	}

	private async void Update()
	{
		if(_currentConvert.Count < 3 && _waitingConvert.Count > 0)
		{
			ConversionObject nextToConvert = _waitingConvert[0];
			_waitingConvert.RemoveAt(0);
			_currentConvert.Add(nextToConvert);
			await nextToConvert.ConvertAsync();
		}
	}

	public void AddFile(string filePath)
	{
		GameObject newConversion = Instantiate(_conversionPrefab, _processingRoot.transform);
		ConversionObject conversionManager = newConversion.GetComponent<ConversionObject>();
		if(conversionManager)
		{
			conversionManager.OnObjectFinished += ConversionFinished;
			conversionManager.OnObjectDeleted += ConversionDeleted;
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
}
