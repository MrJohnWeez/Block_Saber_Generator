using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NonConvertedListManager : MonoBehaviour
{
	[SerializeField] private GameObject _contentRoot = null;
	[SerializeField] private GameObject _tilePrefab = null;

	public void AddZip(UnityWebRequest url)
	{
		GameObject newTile = Instantiate(_tilePrefab, _contentRoot.transform);
		ZippedFile zippedFile = newTile.GetComponent<ZippedFile>();
		if (zippedFile)
		{
			zippedFile.SetWebRequest(url);
		}
	}
}
