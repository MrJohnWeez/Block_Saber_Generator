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
	private List<BeatMapData> _beatMapData = new List<BeatMapData>();

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

			Debug.Log("Parsing files...");
			ParseJson();

			SafeFileManagement.DeleteDirectory(_tempUnZipPath);
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
	private void ConvertFiles()
	{
		string[] files = Directory.GetFiles(_tempFilePath, "*.egg*", SearchOption.AllDirectories);

		foreach(string path in files)
		{
			string newName = path.Replace(".egg", ".ogg");
			SafeFileManagement.MoveFile(path, newName);
		}
	}

	private void ParseJson()
	{
		string infoPath = Path.Combine(_tempUnZipPath, "Info.dat");
		PackInfo packInfo = JsonUtility.FromJson<PackInfo>(SafeFileManagement.GetFileContents(infoPath));
		Debug.Log(packInfo._version);

		_beatMapData.Clear();

		foreach (_difficultyBeatmapSets difficultyBeatmapSets in packInfo._difficultyBeatmapSets)
		{
			foreach(_difficultyBeatmaps difficultyBeatmaps in difficultyBeatmapSets._difficultyBeatmaps)
			{
				string songData = Path.Combine(_tempUnZipPath, difficultyBeatmaps._beatmapFilename);
				BeatMapData beatMapData = JsonUtility.FromJson<BeatMapData>(SafeFileManagement.GetFileContents(songData));
				_beatMapData.Add(beatMapData);
			}
		}

		foreach(BeatMapData bmd in _beatMapData)
		{
			Debug.Log(bmd._notes.Length);
		}
	}
}
