using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
using Minecraft.Generator;

public static class ConvertZip
{
	/// <summary>
	/// Convert a beat saber map zip into two minecraft zip files 
	/// (one datapack and resourcepack)
	/// </summary>
	/// <param name="zipPath">file path to beat saber zip file</param>
	/// <param name="datapackOutputPath">folder path that minecraft zips will be generated</param>
	/// <returns>-1 on Sucess</returns>
	public static int Convert(string zipPath, string datapackOutputPath)
	{
		if (File.Exists(zipPath) && Directory.Exists(datapackOutputPath))
		{
			Debug.Log("Decompressing files...");
			string tempUnZipPath = UnZipFile(zipPath, Application.temporaryCachePath);
			Debug.Log("tempUnZipPath: " + tempUnZipPath);

			Debug.Log("Parsing files...");
			PackInfo packInfo = GetPackInfo(tempUnZipPath);
			List<BeatMapSong> beatMapSongList = ParseBeatSaberDat(tempUnZipPath, packInfo);

			Debug.Log("Converting files...");
			packInfo = ConvertSoundFile(tempUnZipPath, packInfo);
			packInfo = ConvertImageFiles(tempUnZipPath, packInfo);

			if (beatMapSongList.Count > 0 && packInfo != null)
			{
				Debug.Log("Generating Resource pack...");
				ResourcePack.FromBeatSaberData(tempUnZipPath, datapackOutputPath, packInfo);

				//Debug.Log("Generating Data pack...");
				//DatapackGenerator datapackGenerator = new DatapackGenerator(tempUnZipPath, packInfo, beatMapSongList, datapackOutputPath);
				//datapackGenerator.Generate();
			}

			//Debug.Log("Deleting Temp files...");
			//SafeFileManagement.DeleteDirectory(tempUnZipPath);

			Debug.Log("Done.");
			return -1;
		}
		return 0;
	}

	/// <summary>
	/// Unzip the beatsaber map pack into a temp directory
	/// </summary>
	/// <param name="fileToUnzip">path of file to unzip</param>
	/// <param name="pathToUnzip">path to unzip the file into</param>
	/// <returns>the location of the unzipped file</returns>
	public static string UnZipFile(string fileToUnzip, string pathToUnzip)
	{
		string tempUnZipPath = Path.Combine(pathToUnzip, SafeFileManagement.GetFolderName(fileToUnzip));
		if (Directory.Exists(tempUnZipPath))
			SafeFileManagement.DeleteDirectory(tempUnZipPath);

		Directory.CreateDirectory(tempUnZipPath);
		Archive.Decompress(fileToUnzip, tempUnZipPath);
		return tempUnZipPath;
	}

	/// <summary>
	/// Convert egg file to ogg
	/// </summary>
	/// <param name="rootFilePath">Folder that contains .egg files</param>
	/// <param name="packInfo">info object that contains data about beatsaber songs</param>
	/// <returns>updated pack info object</returns>
	public static PackInfo ConvertSoundFile(string rootFilePath, PackInfo packInfo)
	{
		string[] files = Directory.GetFiles(rootFilePath, "*.egg*", SearchOption.AllDirectories);

		foreach (string path in files)
		{
			string newName = path.Replace(".egg", ".ogg");
			packInfo._songFilename = packInfo._songFilename.Replace(".egg", ".ogg");
			SafeFileManagement.MoveFile(path, newName);
		}
		return packInfo;
	}

	/// <summary>
	/// Convert images file to png
	/// </summary>
	/// <param name="rootFilePath">Folder that contains .jpg files</param>
	/// <param name="packInfo">info object that contains data about beatsaber songs</param>
	/// <returns>updated pack info object</returns>
	public static PackInfo ConvertImageFiles(string rootFilePath, PackInfo packInfo)
	{
		string[] files = Directory.GetFiles(rootFilePath, "*.jpg*", SearchOption.AllDirectories);

		foreach (string path in files)
		{
			string newName = path.Replace(".jpg", ".png");
			packInfo._coverImageFilename = packInfo._coverImageFilename.Replace(".jpg", ".png");
			SafeFileManagement.MoveFile(path, newName);
		}
		return packInfo;
	}

	/// <summary>
	/// Read dat files as json files and load into struct
	/// </summary>
	/// <param name="rootFilePath">Folder that contains beatmap files</param>
	/// <param name="packInfo">info object that contains data about beatsaber songs</param>
	/// <returns>List of parsed beatmap data</returns>
	public static List<BeatMapSong> ParseBeatSaberDat(string rootFilePath, PackInfo packInfo)
	{
		List<BeatMapSong> beatMapSongList = new List<BeatMapSong>();

		foreach (_difficultyBeatmapSets difficultyBeatmapSets in packInfo._difficultyBeatmapSets)
		{
			foreach (_difficultyBeatmaps difficultyBeatmaps in difficultyBeatmapSets._difficultyBeatmaps)
			{
				string songData = Path.Combine(rootFilePath, difficultyBeatmaps._beatmapFilename);
				BeatMapData beatMapData = JsonUtility.FromJson<BeatMapData>(SafeFileManagement.GetFileContents(songData));
				beatMapSongList.Add(new BeatMapSong(beatMapData, difficultyBeatmaps));
			}
		}
		return beatMapSongList;
	}

	/// <summary>
	/// Return PackInfo from within a root folder
	/// </summary>
	/// <param name="rootFilePath">folder that contains Info.dat</param>
	/// <returns>Parsed info from Info.dat</returns>
	public static PackInfo GetPackInfo(string rootFilePath)
	{
		string infoPath = Path.Combine(rootFilePath, "Info.dat");
		return JsonUtility.FromJson<PackInfo>(SafeFileManagement.GetFileContents(infoPath));
	}
}
