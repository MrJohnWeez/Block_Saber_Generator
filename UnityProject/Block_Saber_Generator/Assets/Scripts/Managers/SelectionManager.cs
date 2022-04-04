using B83.Win32;
using SFB;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using Utilities;

/// <summary>
/// Manages the files section process and user settings
/// </summary>
[RequireComponent(typeof(Animator))]
public class SelectionManager : MonoBehaviour
{


    [SerializeField] private ProcessManager _processManager = null;
    [SerializeField] private TMP_Text _outputPathText = null;


    private readonly ExtensionFilter[] extensions = { new ExtensionFilter("Beat Saber Song Pack", "zip") };
    private Animator _dragAndDrop = null;
    private const string DEFAULT_FOLDER_NAME = "Converted_Files";


    /// <summary> Path that the files will be saved to </summary>
    public string OutputPath { get; private set; }


    private void Start()
    {
        _dragAndDrop = GetComponent<Animator>();
        OutputPath = Path.Combine(Application.dataPath, DEFAULT_FOLDER_NAME);
        string oldPath = PlayerPrefs.GetString("SavePath");
        if (oldPath != null && !oldPath.IsEmpty())
        {
            OutputPath = oldPath;
        }
        Directory.CreateDirectory(OutputPath);
        Debug.Log("Output Path: " + OutputPath);
        _outputPathText.text = OutputPath;
    }

    protected void OnEnable()
    {
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += FilesDropped;

    }

    protected void OnDisable()
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


    /// <summary>
    /// Open file windows for user to select a new output folder
    /// </summary>
    public void ChangeOutputPath()
    {
        string[] folders = StandaloneFileBrowser.OpenFolderPanel("Select Output Folder", OutputPath, false);
        if (folders.Length > 0)
        {
            OutputPath = folders[0];
            PlayerPrefs.SetString("SavePath", OutputPath);
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
        foreach (string filePath in zipFiles)
        {
            _processManager.AddFile(filePath);
        }
    }

    /// <summary>
    /// User has dropped files in the unity window (only works in standalone)
    /// </summary>
    /// <param name="aFiles">File list</param>
    /// <param name="aPos">Postion of mouse on file drop</param>
    private void FilesDropped(List<string> aFiles, POINT aPos)
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
