using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertedListManager : MonoBehaviour
{

	[SerializeField] private GameObject _contentRoot = null;
	[SerializeField] private GameObject _tilePrefab = null;

	public void AddConvertedFile(string iconURL, string title, string resourcepackURL, string datapackURL)
	{
		GameObject newTile = Instantiate(_tilePrefab, _contentRoot.transform);
		ConvertedFile convertedFile = newTile.GetComponent<ConvertedFile>();
		if(convertedFile)
		{
			convertedFile.SetTitle(title);
			convertedFile.SetImage(iconURL);
			convertedFile.SetURLS(datapackURL, resourcepackURL);
		}
	}
}
