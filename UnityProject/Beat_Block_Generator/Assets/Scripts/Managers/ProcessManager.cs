using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager : MonoBehaviour
{
	[SerializeField] private GameObject _conversionPrefab = null;
	[SerializeField] private GameObject _processingRoot = null;
	
	public void AddFile(string filePath)
	{
		GameObject newConversion = Instantiate(_conversionPrefab, _processingRoot.transform);
		ConversionObject conversionManager = newConversion.GetComponent<ConversionObject>();
		if(conversionManager)
		{
			conversionManager.filePath = filePath;
		}
	}
}
