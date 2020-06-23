using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonConvertedListManager : MonoBehaviour
{
	[SerializeField] private GameObject _contentRoot = null;
	[SerializeField] private GameObject _tilePrefab = null;

	public void AddZips(string[] files)
	{
		foreach(string filePath in files)
		{
			GameObject newTile = Instantiate(_tilePrefab, _contentRoot.transform);
			ZippedFile zippedFile = newTile.GetComponent<ZippedFile>();
			if (zippedFile)
			{
				zippedFile.SetTitle(filePath);
				zippedFile.SetURL(filePath);
			}
		}
	}
}
