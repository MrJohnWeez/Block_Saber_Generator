using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class Converter
{
	// Paths
	private string _tempFilePath = Application.temporaryCachePath;
	private string _zipPath = "";
	private string _datapackOutputPath = "";
	private string _tempUnZipPath = "";

	// Map Pack info

	private PackInfo _packInfo;
	private List<BeatMapSong> _beatMapSongList = new List<BeatMapSong>();

	

	public Converter(string zipPath, string datapackOutputPath)
	{
		_zipPath = zipPath;
		_datapackOutputPath = datapackOutputPath;
	}

	public void GenerateMinecraftResources()
	{
		if(File.Exists(_zipPath) && Directory.Exists(_datapackOutputPath))
		{
			Debug.Log(_tempFilePath);
			Debug.Log("Decompressing files...");
			UnZipFile();
			
			Debug.Log("Parsing files...");
			ParseBeatSaberDat();

			Debug.Log("Converting files...");
			ConvertSoundFile();
			ConvertImageFiles();

			if (_beatMapSongList.Count > 0 && _packInfo != null)
			{
				Debug.Log("Generating Resource pack...");
				ResourcepackGenerator resourcepackGenerator = new ResourcepackGenerator(_tempUnZipPath, _packInfo, _datapackOutputPath);
				resourcepackGenerator.Generate();

				Debug.Log("Generating Data pack...");
				DatapackGenerator datapackGenerator = new DatapackGenerator(_tempUnZipPath, _packInfo, _beatMapSongList, _datapackOutputPath);
				datapackGenerator.Generate();
			}

			Debug.Log("Deleting Temp files...");
			SafeFileManagement.DeleteDirectory(_tempUnZipPath);

			Debug.Log("Done.");
		}		
	}

	/// <summary>
	/// Unzip the beatsaber map pack into a temp directory
	/// </summary>
	private void UnZipFile()
	{
		_tempUnZipPath = Path.Combine(_tempFilePath, SafeFileManagement.GetFolderName(_zipPath) + SafeFileManagement.GetDateNow());
		Directory.CreateDirectory(_tempUnZipPath);
		Archive.Decompress(_zipPath, _tempUnZipPath);
	}

	/// <summary>
	/// Convert egg file to ogg
	/// </summary>
	private void ConvertSoundFile()
	{
		string[] files = Directory.GetFiles(_tempUnZipPath, "*.egg*", SearchOption.AllDirectories);

		foreach(string path in files)
		{
			string newName = path.Replace(".egg", ".ogg");
			_packInfo._songFilename = _packInfo._songFilename.Replace(".egg", ".ogg");
			SafeFileManagement.MoveFile(path, newName);
		}
	}

	/// <summary>
	/// Convert images file to png
	/// </summary>
	private void ConvertImageFiles()
	{
		string[] files = Directory.GetFiles(_tempUnZipPath, "*.jpg*", SearchOption.AllDirectories);

		foreach (string path in files)
		{
			string newName = path.Replace(".jpg", ".png");
			_packInfo._coverImageFilename = _packInfo._coverImageFilename.Replace(".jpg", ".png");
			SafeFileManagement.MoveFile(path, newName);
		}
	}

	/// <summary>
	/// Read dat files as json files and load into struct
	/// </summary>
	private void ParseBeatSaberDat()
	{
		string infoPath = Path.Combine(_tempUnZipPath, "Info.dat");
		_packInfo = JsonUtility.FromJson<PackInfo>(SafeFileManagement.GetFileContents(infoPath));
		_beatMapSongList.Clear();

		foreach (_difficultyBeatmapSets difficultyBeatmapSets in _packInfo._difficultyBeatmapSets)
		{
			foreach(_difficultyBeatmaps difficultyBeatmaps in difficultyBeatmapSets._difficultyBeatmaps)
			{
				string songData = Path.Combine(_tempUnZipPath, difficultyBeatmaps._beatmapFilename);
				BeatMapData beatMapData = JsonUtility.FromJson<BeatMapData>(SafeFileManagement.GetFileContents(songData));
				_beatMapSongList.Add(new BeatMapSong(beatMapData, difficultyBeatmaps));
			}
		}
	}
}
