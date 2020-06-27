using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ConversionObject : MonoBehaviour
{
	public delegate void ObjectEvent(ConversionObject conversionObject);
	public event ObjectEvent OnObjectConverted;
	public event ObjectEvent OnObjectDeleted;

	[HideInInspector] public string filePath = "";
	[HideInInspector] public string outputPath = "";
	[HideInInspector] public int uuid = -1;

	[SerializeField] private TMP_Text title = null;
	[SerializeField] private TMP_Text _status = null;
	[SerializeField] private Slider _progressBar = null;
	[SerializeField] private Image _trash = null;
	[SerializeField] private Sprite _trashOpen = null;
	[SerializeField] private Sprite _trashClosed = null;
	private string fileName = "";

    void Start()
    {
		fileName = Path.GetFileNameWithoutExtension(filePath);
		title.text = fileName;
		_status.text = "Waiting...";
		_progressBar.value = 0;
	}

	public void Convert()
	{

		OnObjectConverted?.Invoke(this);
	}

	public void DeleteSelf()
	{
		OnObjectDeleted?.Invoke(this);
		Destroy(gameObject);
	}

	public void TrashOpen()
	{
		_trash.sprite = _trashOpen;
	}

	public void TrashClosed()
	{
		_trash.sprite = _trashClosed;
	}
}
