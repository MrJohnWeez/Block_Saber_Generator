// Created by MrJohnWeez
// June 2020

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
using Minecraft.Generator;
using BeatSaber;
using BeatSaber.packInfo;
using BeatSaber.packInfo.difficultyBeatmapSets;
using BeatSaber.beatMapData.obstacles.BeatSaber.BeatMapData;
using BeatSaber.packInfo.difficultyBeatmapSets.difficultyBeatMaps;

namespace Minecraft
{
	/// <summary>
	/// Tools to read in a Beat Saber Zip file and output a Minecraft Resourcepack and Datapack
	/// </summary>
	public static class ConvertZip
	{
		/// <summary>
		/// Convert a Beat Saber Zip file into a Minecraft Resourcepack and Datapack
		/// </summary>
		/// <param name="zipPath">file path to beat saber zip file</param>
		/// <param name="datapackOutputPath">folder path that minecraft zips will be generated</param>
		/// <returns>-1 on Sucess</returns>
		public static Task<int> ConvertAsync(string zipPath, string datapackOutputPath, int uuid, CancellationToken cancellationToken)
		{
			return Task.Run(async () =>
			{
				if (File.Exists(zipPath) && Directory.Exists(datapackOutputPath))
				{
					// Decompressing files
					string tempUnZipPath = await UnZipFileAsync(zipPath, ProcessManager.temporaryPath, cancellationToken);
					try
					{
						// Parsing files
						PackInfo packInfo = GetPackInfo(tempUnZipPath);
						packInfo._uuid = uuid;
						List<BeatMapSong> beatMapSongList = await ParseBeatSaberDat(tempUnZipPath, packInfo);
						
						// Converting files
						packInfo = ConvertSoundFile(tempUnZipPath, packInfo);
						packInfo = ConvertImageFilesAsync(tempUnZipPath, packInfo);

						cancellationToken.ThrowIfCancellationRequested();

						if (beatMapSongList.Count > 0 && packInfo != null)
						{
							// Generating Resource pack
							int failCode = await ResourcePack.FromBeatSaberData(tempUnZipPath, datapackOutputPath, packInfo);
							cancellationToken.ThrowIfCancellationRequested();

							// Generating Data pack
							failCode = await DataPack.FromBeatSaberData(tempUnZipPath, datapackOutputPath, packInfo, beatMapSongList, cancellationToken);
							return -1;
						}
						return 0;
						
					}
					catch (OperationCanceledException wasCanceled)
					{
						SafeFileManagement.DeleteDirectory(tempUnZipPath);
					}
					catch (ObjectDisposedException wasAreadyCanceled)
					{
						SafeFileManagement.DeleteDirectory(tempUnZipPath);
					}
				}
				return 0;
			});
		}

		/// <summary>
		/// Unzip the beatsaber map pack into a temp directory
		/// </summary>
		/// <param name="fileToUnzip">path of file to unzip</param>
		/// <param name="pathToUnzip">path to unzip the file into</param>
		/// <returns>the location of the unzipped file</returns>
		public static Task<string> UnZipFileAsync(string fileToUnzip, string pathToUnzip, CancellationToken cancellationToken)
		{
			return Task.Run(async () =>
			{
				string tempUnZipPath = Path.Combine(pathToUnzip, SafeFileManagement.GetFolderName(fileToUnzip));
				if (Directory.Exists(tempUnZipPath))
					SafeFileManagement.DeleteDirectory(tempUnZipPath);

				Directory.CreateDirectory(tempUnZipPath);
				await Archive.DecompressAsync(fileToUnzip, tempUnZipPath, cancellationToken);
				return tempUnZipPath;
			});
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
		public static PackInfo ConvertImageFilesAsync(string rootFilePath, PackInfo packInfo)
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
	public static Task<List<BeatMapSong>> ParseBeatSaberDat(string rootFilePath, PackInfo packInfo)
	{
		return Task.Run(() =>
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
		});
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
}
