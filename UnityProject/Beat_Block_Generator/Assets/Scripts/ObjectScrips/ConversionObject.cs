using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ConversionObject : MonoBehaviour
{
	[HideInInspector] public string filePath = "";
	[HideInInspector] public string outputPath = "";

	[SerializeField] private TMP_Text title = null;
	[SerializeField] private TMP_Text _status = null;
	[SerializeField] private Slider _progressBar = null;
	[SerializeField] private Image _trash = null;
	[SerializeField] private Image _trashOpen = null;
	[SerializeField] private Image _trashClosed = null;
	private string fileName = "";

    void Start()
    {
		fileName = Path.GetFileNameWithoutExtension(filePath);
		title.text = fileName;
		_status.text = "Waiting...";
		_progressBar.value = 0;
	}

	public void DeleteSelf()
	{
		Destroy(gameObject);
	}

	public void TrashOpen()
	{
		_trash = _trashOpen;
	}

	public void TrashClosed()
	{
		_trash = _trashClosed;
	}
}
