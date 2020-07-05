// Created by MrJohnWeez
// June 2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Minecraft;
using System.Threading;
using System.Threading.Tasks;

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
	private CancellationTokenSource _asyncSourceCancel = null;

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

	public async Task ConvertAsync()
	{
		_progressBar.value = 0;
		_asyncSourceCancel = new CancellationTokenSource();
		int uuid = UnityEngine.Random.Range(-99999999, 99999999);
		int errorCode = await ConvertZip.ConvertAsync(InputPath, OutputPath, uuid, _asyncSourceCancel.Token);
		if(errorCode > 0)
		{
			Debug.Log("There was error: " + errorCode);
			Failed();
		}
		else
		{
			Finished();
		}
		_asyncSourceCancel.Dispose();
		_asyncSourceCancel = null;
		OnObjectFinished?.Invoke(this);
	}


	#region ButtonFunctions
	public void DeleteSelf()
	{
		if(_asyncSourceCancel != null)
			_asyncSourceCancel?.Cancel();
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
