using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcepackGenerator
{
	private readonly string[] _excludeExtensions = { ".meta" };
	private const int C_numberOfIORetryAttempts = 5;

	// Names
	private const string C_TemplateName = @"SONG_AUTHOR - SONG_NAME";
	private const string C_PackIconName = "pack.png";
	private const string C_PackMetaName = "pack.mcmeta";



	// Paths
	private string _pathOfResourcepackTemplate = Path.Combine(Application.dataPath, "StreamingAssets", "TemplateResourcepack");
	
	private string _unzippedFolderPath = "";
	private string _newResourcepackPath = "";
	private string _datapackOutputPath = "";

	private Dictionary<string, string> _keyVars = new Dictionary<string, string>();

	private PackInfo _packInfo;

	public ResourcepackGenerator(string unzippedFolderPath, PackInfo packInfo, string datapackOutputPath)
	{
		this._unzippedFolderPath = unzippedFolderPath;
		this._packInfo = packInfo;
		this._datapackOutputPath = datapackOutputPath;
		SetKeyVars();
	}

	public void Generate()
	{
		if(Directory.Exists(_unzippedFolderPath) && _packInfo != null)
		{
			Debug.Log("Copying Template...");
			if (CopyTemplate())
			{
				Debug.Log("Copying Image Icon...");
				CopyMapIcon();

				Debug.Log("Change meta data...");
				EditMcMeta();

				Debug.Log("Copying song...");
				CopySong();

				Debug.Log("Copying and deleting resourcepack...");
				CopyAndDeleteTemp();
			}
		}
	}

	private void SetKeyVars()
	{
		_keyVars["MAPPER_NAME"] = _packInfo._levelAuthorName;
		_keyVars["BEATS_PER_MINUTE"] = _packInfo._beatsPerMinute.ToString();
	}

	private bool CopyTemplate()
	{
		if(SafeFileManagement.DirectoryCopy(_pathOfResourcepackTemplate, _unzippedFolderPath, true, _excludeExtensions, C_numberOfIORetryAttempts))
		{
			string copiedTemplatePath = Path.Combine(_unzippedFolderPath, C_TemplateName);
			string resourcepackName = _packInfo._songAuthorName + " - " + _packInfo._songName + " " + _packInfo._songSubName;
			_newResourcepackPath = Path.Combine(_unzippedFolderPath, resourcepackName);
			return SafeFileManagement.MoveDirectory(copiedTemplatePath, _newResourcepackPath, C_numberOfIORetryAttempts);
		}
		return false;
	}

	private bool CopyMapIcon()
	{
		string mapIcon = Path.Combine(_unzippedFolderPath, _packInfo._coverImageFilename);
		string packIcon = Path.Combine(_newResourcepackPath, C_PackIconName);
		return SafeFileManagement.CopyFileTo(mapIcon, packIcon, true, C_numberOfIORetryAttempts);
	}

	private void EditMcMeta()
	{
		string metaPath = Path.Combine(_newResourcepackPath, C_PackMetaName);
		string metaText = SafeFileManagement.GetFileContents(metaPath);
		foreach(string key in _keyVars.Keys)
		{
			metaText = metaText.Replace(key, _keyVars[key]);
		}

		SafeFileManagement.SetFileContents(metaPath, metaText);
	}

	private void CopySong()
	{

	}

	private void CopyAndDeleteTemp()
	{

	}
}
