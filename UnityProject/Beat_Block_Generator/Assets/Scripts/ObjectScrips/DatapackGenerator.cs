using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DatapackGenerator : GeneratorBase
{
	// Datapack hardcoded names
	private const string C_Data = "data";
	private const string C_BlockSaber = "Block_Saber";
	private const string C_Functions = "functions";
	private const string C_McFunction = ".mcfunction";
	private const string C_Tags = "tags";
	private const string C_FakePlayerChar = "#";
	private const string C_Slash = "/";

	private const string C_TemplateName = @"BlockSaberTemplate";

	private string _pathOfDatapackTemplate = Path.Combine(C_StreamingAssets, "TemplateDatapack");

	private List<BeatMapSong> _beatMapSongList;
	private string _datapackOutputPath = "";
	

	public DatapackGenerator(string unzippedFolderPath, PackInfo packInfo, List<BeatMapSong> beatMapSongList, string datapackOutputPath)
	{
		this._unzippedFolderPath = unzippedFolderPath;
		this._packInfo = packInfo;
		this._beatMapSongList = beatMapSongList;
		this._datapackOutputPath = datapackOutputPath;
		Init();
	}
	
	public override bool Generate()
	{
		if (Directory.Exists(_unzippedFolderPath) && _packInfo != null && _beatMapSongList.Count > 0)
		{
			Debug.Log("Copying Template...");
			if(CopyTemplate(_pathOfDatapackTemplate, _unzippedFolderPath))
			{
				UpdateMcMetaWithKeys();
				Debug.Log("Datapack Done");

				// execute if score @s TickCount matches MIN..MAX run function FOLDER_NAME:FUNCTION_NAME
				return true;
			}
		}
		return false;
	}

	protected override bool CopyTemplate(string sourceDirName, string destDirName)
	{
		if (base.CopyTemplate(sourceDirName, destDirName))
		{
			string copiedTemplatePath = Path.Combine(destDirName, C_TemplateName);
			_rootFolderPath = Path.Combine(destDirName, _packName);
			return SafeFileManagement.MoveDirectory(copiedTemplatePath, _rootFolderPath, C_numberOfIORetryAttempts);
		}
		return false;
	}

	protected override void Init()
	{
		base.Init();
		_longNameID = _packInfo._songAuthorName + " - " + _packInfo._songName + " " + _packInfo._songSubName;
		_packName = C_BlockSaber + _packInfo._songAuthorName + " - " + _packInfo._songName + " " + _packInfo._songSubName;
		_fullOutputPath = Path.Combine(_outputPath, _packName + C_Zip);
		_keyVars["MAPPER_NAME"] = _packInfo._levelAuthorName;
		_keyVars["BEATS_PER_MINUTE"] = _packInfo._beatsPerMinute.ToString();
		_keyVars["SONGUUID"] = Random.Range(-999999999, 999999999).ToString();
	}

	/// <summary>
	/// Update McMeta with key values
	/// </summary>
	private void UpdateMcMetaWithKeys()
	{
		string metaPath = Path.Combine(_rootFolderPath, C_PackMeta);
		UpdateFileWithKeys(metaPath);
	}
}
