using System.Collections;
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

	public override bool Generate()
	{
		if (Directory.Exists(_unzippedFolderPath) && _packInfo != null)
		{
			Debug.Log("Copying Template...");
			if (CopyTemplate(_pathOfResourcepackTemplate, _unzippedFolderPath))
			{
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

	protected override void Init()
	{
		base.Init();
		_folder_uuid = SafeFileManagement.GetFileName(Path.GetFileName(_unzippedFolderPath)).MakeMinecraftSafe();
		_packName = C_ResourcePack + _folder_uuid;
		_fullOutputPath = Path.Combine(_outputPath, _packName + C_Zip);
		_keyVars["SONG"] = _folder_uuid;
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
