using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class ZippedFile : MonoBehaviour
{
	public Image image = null;
	public TMP_Text title = null;
	public UnityWebRequest request = null;

	public void SetWebRequest(UnityWebRequest urlToFile)
	{
		request = urlToFile;
		title.text = request.url;
	}

	public void Convert()
	{
		Debug.Log("Converting Pressed!");
		Converter _converter = new Converter(request.url);
		_converter.GenerateMinecraftResources();
	}
}
