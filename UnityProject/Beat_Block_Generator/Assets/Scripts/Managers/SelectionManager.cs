using B83.Win32;
using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Animator))]
public class SelectionManager : MonoBehaviour
{
	public string OutputPath
	{
		get;
		private set;
	}

	[SerializeField] private ProcessManager _processManager = null;
	[SerializeField] private TMP_Text _outputPathText = null;
	private readonly ExtensionFilter[] extensions = { new ExtensionFilter("Beat Saber Song Pack", "zip") };
	private Animator _dragAndDrop = null;
	private const string DEFAULT_FOLDER_NAME = "Converted_Files";


	#region UnityCallbacks
	private void Start()
	{
		_dragAndDrop = GetComponent<Animator>();
		OutputPath = Path.Combine(Application.dataPath, DEFAULT_FOLDER_NAME);
		OutputPath = @"C:\Users\John\Desktop\Converted_Files";
		Directory.CreateDirectory(OutputPath);
		Debug.Log("Output Path: " + OutputPath);
		_outputPathText.text = OutputPath;
	}

	void OnEnable()
	{
		UnityDragAndDropHook.InstallHook();
		UnityDragAndDropHook.OnDroppedFiles += FilesDropped;

	}
	void OnDisable()
	{
		UnityDragAndDropHook.UninstallHook();
	}

	public void PointerEnter()
	{
		_dragAndDrop.SetBool("PointerEntered", true);
	}

	public void PointerExit()
	{
		_dragAndDrop.SetBool("PointerEntered", false);
	}
	#endregion UnityCallbacks
	
	public void ChangeOutputPath()
	{
		string[] folders = StandaloneFileBrowser.OpenFolderPanel("Select Output Folder", OutputPath, false);
		if(folders.Length > 0)
		{
			OutputPath = folders[0];
			_outputPathText.text = OutputPath;
		}
	}

	public void OpenOutputPath()
	{
		SafeFileManagement.OpenFolder(OutputPath);
	}


	public void PointerClicked()
	{
		string[] zipFiles = StandaloneFileBrowser.OpenFilePanel("Select Beat Saber Map Zip", "", extensions, true);
		foreach(string filePath in zipFiles)
		{
			_processManager.AddFile(filePath);
		}
	}

	void FilesDropped(List<string> aFiles, POINT aPos)
	{
		foreach (var filePath in aFiles)
		{
			FileInfo info = new FileInfo(filePath);
			var ext = info.Extension.ToLower();
			if (ext == ".zip")
			{
				_processManager.AddFile(filePath);
			}
		}
	}
}
