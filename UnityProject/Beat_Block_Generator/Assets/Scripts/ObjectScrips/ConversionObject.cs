using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ConversionObject : MonoBehaviour
{
	public delegate void ObjectEvent(ConversionObject conversionObject);
	public event ObjectEvent OnObjectFinished;
	public event ObjectEvent OnObjectDeleted;

	#region Properties
	public string InputPath
	{
		get;
		private set;
	}

	public string OutputPath
	{
		get;
		private set;
	}

	public string FileName
	{
		get;
		private set;
	}
	#endregion Properties

	[Header("Progress Bar Colors")]
	[SerializeField] private Color _finishSuccess;
	[SerializeField] private Color _finishFail;

	[Header("Connected Objects")]
	[SerializeField] private TMP_Text title = null;
	[SerializeField] private TMP_Text _status = null;
	[SerializeField] private Slider _progressBar = null;
	[SerializeField] private Image _progressFill = null;
	[SerializeField] private Image _trash = null;

	[Header("Toggle Images")]
	[SerializeField] private Sprite _trashOpen = null;
	[SerializeField] private Sprite _trashClosed = null;
	private Converter _converter = null;

	/// <summary>
	/// Sets up the ConversionObject object ready for conversion
	/// </summary>
	/// <param name="inputPath">file path of the zip file to convert</param>
	/// <param name="outputFolder">folder path to output the two minecraft zip files</param>
	public void Setup(string inputPath, string outputFolder)
	{
		InputPath = inputPath;
		OutputPath = outputFolder;
		FileName = Path.GetFileNameWithoutExtension(inputPath);

		title.text = FileName;
		_status.text = "Waiting...";
		_progressBar.value = 0;
	}

	public void Convert()
	{
		_progressBar.value = 0;
		_converter = new Converter(InputPath, OutputPath);
		int errorCode = _converter.GenerateMinecraftResources();
		if(errorCode > 0)
		{
			Debug.Log("There was error: " + errorCode);
			Failed();
		}
		else
		{
			Finished();
		}
		OnObjectFinished?.Invoke(this);
	}


	#region ButtonFunctions
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
	#endregion ButtonFunctions
	

	private void Finished()
	{
		_progressBar.value = _progressBar.maxValue;
		_progressFill.color = _finishSuccess;
		_status.text = "Done ";
	}

	private void Failed()
	{
		_progressBar.value = _progressBar.maxValue;
		_progressFill.color = _finishFail;
		_status.text = "Failed! ";
	}
}
