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
	public UnityWebRequest url = null;

	public void SetWebRequest(UnityWebRequest urlToFile)
	{
		url = urlToFile;
	}

	public void Convert()
	{
		//_converter = new Converter(url.ToString());
		//_converter.GenerateMinecraftResources();
	}
}
