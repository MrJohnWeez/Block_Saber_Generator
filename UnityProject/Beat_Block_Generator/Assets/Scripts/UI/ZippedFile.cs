using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZippedFile : MonoBehaviour
{
	public Image image = null;
	public TMP_Text title = null;
	public string zipFilekURL = "";

	public void SetTitle(string newTitle)
	{
		title.text = newTitle;
	}

	public void SetURL(string url)
	{
		zipFilekURL = url;
	}
}
