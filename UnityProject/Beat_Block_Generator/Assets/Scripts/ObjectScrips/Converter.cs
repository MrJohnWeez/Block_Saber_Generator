using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class Converter
{
	private readonly string[] _excludeExtensions = { ".meta" };

	private const int C_numberOfIORetryAttempts = 5;

	// Datapack hardcoded names
	private const string C_Data = "data";
	private const string C_Minecraft = "minecraft";
	private const string C_Functions = "functions";
	private const string C_McFunction = ".mcfunction";
	private const string C_Tags = "tags";
	private const string C_FakePlayerChar = "#";
	private const string C_Slash = "/";

	private string _tempFilePath = Application.temporaryCachePath;
	private string _unityDataPath = Application.dataPath;

	private Dictionary<string, string> _keyVars = new Dictionary<string, string>();

	private string _zipPath = "";
	private string _datapackOutputPath = "";
	private string _tempUnZipPath = "";

	public Converter(string zipPath, string datapackOutputPath)
	{

		_zipPath = zipPath;
		_datapackOutputPath = datapackOutputPath;
	}

	public void GenerateDatapack()
	{
		if(File.Exists(_zipPath) && Directory.Exists(_datapackOutputPath))
		{
			Debug.Log(_tempFilePath);
			Debug.Log("Decompressing files...");
			UnZipFile();

			Debug.Log("Converting files...");
			ConvertFiles();


			//SafeFileManagement.DeleteDirectory(_tempUnZipPath);
			Debug.Log("Done.");
		}


		//string pathOfDatapackTemplate = Path.Combine(dataStats.unityDataPath, "StreamingAssets", "CopyTemplate");
	}

	private void UnZipFile()
	{
		_tempUnZipPath = Path.Combine(_tempFilePath, SafeFileManagement.GetFolderName(_zipPath) + SafeFileManagement.GetDateNow());
		Directory.CreateDirectory(_tempUnZipPath);
		Archive.Decompress(_zipPath, _tempUnZipPath);
	}

	/// <summary>
	/// Convert egg file to ogg
	/// </summary>
	/// <returns>True if successful</returns>
	private bool ConvertFiles()
	{
		string[] songPath = Directory.GetFiles(_tempFilePath, "*.egg", SearchOption.AllDirectories);
		if (songPath.Length > 0)
		{
			string firstSongPath = songPath[0];
			string newName = firstSongPath.Replace(".egg", ".ogg");
			return SafeFileManagement.MoveFile(firstSongPath, newName);
		}
		return false;
	}
}
