using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ConvertedFile : MonoBehaviour
{
	public Image image = null;
	public TMP_Text title = null;
	public string datapackURL = "";
	public string resourcepackURL = "";

	public void SetImage(string url)
	{
		
	}

	public void SetTitle(string newTitle)
	{
		title.text = newTitle;
	}

	public void SetURLS(string datapack, string resourcepack)
	{
		datapackURL = datapack;
		resourcepackURL = resourcepack;
	}
}
