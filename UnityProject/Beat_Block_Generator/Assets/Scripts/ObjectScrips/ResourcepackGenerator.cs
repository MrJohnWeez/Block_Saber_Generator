﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourcepackGenerator : GeneratorBase
{
	// Extension types
	private const string C_Ogg = ".ogg";

	// File Names
	private const string C_PackIcon = "pack.png";
	private const string C_SoundsJson = "sounds.json";

	// Folder Names
	private const string C_TemplateName = @"SONG_AUTHOR - SONG_NAME";
	private const string C_Assets = "assets";
	private const string C_Sounds = "sounds";
	private const string C_Custom = "custom";
	private const string C_ResourcePack = "ResourcePack_";

	// Paths
	private string _pathOfResourcepackTemplate = Path.Combine(C_StreamingAssets, "TemplateResourcepack");

	public ResourcepackGenerator(string unzippedFolderPath, PackInfo packInfo, string datapackOutputPath)
	{
		this._unzippedFolderPath = unzippedFolderPath;
		this._packInfo = packInfo;
		this._outputPath = datapackOutputPath;
		Init();
	}

	/// <summary>
	/// Generate resourcepack
	/// </summary>
	/// <returns></returns>
	public override bool Generate()
	{
		if (Directory.Exists(_unzippedFolderPath) && _packInfo != null)
		{
			Debug.Log("Copying Template...");
			if (CopyTemplate(_pathOfResourcepackTemplate, _unzippedFolderPath))
			{
				UpdateAllCopiedFiles(_rootFolderPath);

				Debug.Log("Copying Image Icon...");
				CopyMapIcon();

				Debug.Log("Copying Song...");
				CopySong();

				Debug.Log("Creating Zip...");
				CreateArchive(_rootFolderPath, _fullOutputPath);

				Debug.Log("Resource Pack Done");
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Copy the Resourcepack template and rename
	/// </summary>
	/// <param name="sourceDirName">Source folder path to copy</param>
	/// <param name="destDirName">Path to copy to</param>
	protected override bool CopyTemplate(string sourceDirName, string destDirName)
	{
		if(base.CopyTemplate(sourceDirName, destDirName))
		{
			string copiedTemplatePath = Path.Combine(destDirName, C_TemplateName);
			_rootFolderPath = Path.Combine(destDirName, _packName);
			return SafeFileManagement.MoveDirectory(copiedTemplatePath, _rootFolderPath, C_numberOfIORetryAttempts);
		}
		return false;
	}

	/// <summary>
	/// Set up varibles and paths based on class data
	/// </summary>
	protected override void Init()
	{
		base.Init();
		_folder_uuid = SafeFileManagement.GetFileName(Path.GetFileName(_unzippedFolderPath)).MakeMinecraftSafe();
		_packName = C_ResourcePack + _folder_uuid;
		_fullOutputPath = Path.Combine(_outputPath, _packName + C_Zip);
		_keyVars["SONGUUID"] = _folder_uuid;
		_keyVars["SONGNAME"] = _packInfo._songName + _packInfo._songSubName;
		_keyVars["AUTHORNAME"] = _packInfo._songAuthorName;
	}

	/// <summary>
	/// Copy the cover icon in the beat map to the minecraft resource pack icon
	/// </summary>
	/// <returns>True if successful</returns>
	private bool CopyMapIcon()
	{
		string mapIcon = Path.Combine(_unzippedFolderPath, _packInfo._coverImageFilename);
		string packIcon = Path.Combine(_rootFolderPath, C_PackIcon);
		return SafeFileManagement.CopyFileTo(mapIcon, packIcon, true, C_numberOfIORetryAttempts);
	}

	/// <summary>
	/// Update all files within a directory to correct varible names
	/// </summary>
	/// <param name="folderPath">In folder path</param>
	private void UpdateAllCopiedFiles(string folderPath, bool checkSubDirectories = false)
	{
		if (checkSubDirectories)
		{
			string[] dirs = SafeFileManagement.GetDirectoryPaths(folderPath, C_numberOfIORetryAttempts);
			foreach (string dir in dirs)
			{
				UpdateAllCopiedFiles(dir, checkSubDirectories);
			}
		}

		if (Directory.Exists(folderPath))
		{
			string[] files = SafeFileManagement.GetFilesPaths(folderPath, C_numberOfIORetryAttempts);
			foreach (string file in files)
			{
				UpdateFileWithKeys(file);
			}
		}
	}

	/// <summary>
	/// Copy the song file from the beat map into the resource pack
	/// </summary>
	/// <returns>True if successful</returns>
	private bool CopySong()
	{
		string mapSong = Path.Combine(_unzippedFolderPath, _packInfo._songFilename);
		string minecraftNamespace = Path.Combine(_rootFolderPath, C_Assets, C_Minecraft);
		string packSong = Path.Combine(minecraftNamespace, C_Sounds, C_Custom, _folder_uuid + C_Ogg);
		if(SafeFileManagement.CopyFileTo(mapSong, packSong, true, C_numberOfIORetryAttempts))
		{
			UpdateFileWithKeys(Path.Combine(minecraftNamespace, C_SoundsJson));
		}
		return false;
	}
}
