using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager : MonoBehaviour
{
	[SerializeField] private GameObject _conversionPrefab = null;
	[SerializeField] private GameObject _processingRoot = null;
	private Queue<ConversionObject> _waitingConvert = new Queue<ConversionObject>();
	private Queue<ConversionObject> _currentConvert = new Queue<ConversionObject>();
	private Queue<ConversionObject> _finishedConvert = new Queue<ConversionObject>();

	private int _nextUUID = 0;
	
	public void AddFile(string filePath)
	{
		GameObject newConversion = Instantiate(_conversionPrefab, _processingRoot.transform);
		ConversionObject conversionManager = newConversion.GetComponent<ConversionObject>();
		if(conversionManager)
		{
			conversionManager.filePath = filePath;
			conversionManager.uuid = _nextUUID;
			conversionManager.OnObjectConverted += ConversionFinished;
			conversionManager.OnObjectDeleted += ConversionDeleted;
			_waitingConvert.Enqueue(conversionManager);
			_nextUUID++;
		}
	}

	public void ConversionFinished(ConversionObject conversionObject)
	{
		Debug.Log("Conversion Finished for id: " + conversionObject.uuid);
	}

	public void ConversionDeleted(ConversionObject conversionObject)
	{
		Debug.Log("Deleted item with id: " + conversionObject.uuid);
	}
}
