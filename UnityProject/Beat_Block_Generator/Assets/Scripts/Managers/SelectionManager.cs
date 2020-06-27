using B83.Win32;
using SFB;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SelectionManager : MonoBehaviour
{
	[SerializeField] private ProcessManager _processManager = null;

	private readonly ExtensionFilter[] extensions = { new ExtensionFilter("Beat Saber Song Pack", "zip") };
	private Animator _dragAndDrop = null;

	private void Start()
	{
		_dragAndDrop = GetComponent<Animator>();
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
