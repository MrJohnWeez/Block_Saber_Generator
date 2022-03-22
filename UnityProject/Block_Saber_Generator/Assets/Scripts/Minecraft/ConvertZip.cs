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

namespace Minecraft
{
    /// <summary>
    /// Tools to read in a Beat Saber Zip file and output a Minecraft Resourcepack and Datapack
    /// </summary>
    public static class ConvertZip
    {
        // /// <summary>
        // /// Convert a Beat Saber Zip file into a Minecraft Resourcepack and Datapack
        // /// </summary>
        // /// <param name="zipPath">file path to beat saber zip file</param>
        // /// <param name="datapackOutputPath">folder path that minecraft zips will be generated</param>
        // /// <param name="uuid">Unique number that determines song value</param>
        // /// <param name="cancellationToken">Token that allows async function to be canceled</param>
        // /// <returns>-1 on Sucess</returns>
        // public static Task<int> ConvertAsync(string zipPath, string datapackOutputPath, int uuid, CancellationToken cancellationToken)
        // {
        //     return Task.Run(async () =>
        //     {
        //         if (File.Exists(zipPath) && Directory.Exists(datapackOutputPath))
        //         {
        //             // Decompressing files
        //             string tempUnZipPath = await UnZipFileAsync(zipPath, ProcessManager.temporaryPath, cancellationToken);

        //             // Parsing files
        //             Info packInfo = GetPackInfo(tempUnZipPath);
        //             if (packInfo != null)
        //             {
        //                 try
        //                 {
        //                     packInfo._uuid = uuid;
        //                     List<BeatMapSong> beatMapSongList = await ParseBeatSaberDat(tempUnZipPath, packInfo);


        //                     // Converting files
        //                     packInfo = ConvertSoundFile(tempUnZipPath, packInfo);
        //                     if (packInfo == null)
        //                         return 4;

        //                     packInfo = ConvertImageFiles(tempUnZipPath, packInfo);

        //                     cancellationToken.ThrowIfCancellationRequested();

        //                     if (beatMapSongList.Count > 0 && packInfo != null)
        //                     {
        //                         // Generating Resource pack
        //                         int failCode = await ResourcePack.FromBeatSaberData(tempUnZipPath, datapackOutputPath, packInfo);
        //                         if (failCode >= 0)
        //                             return failCode;

        //                         cancellationToken.ThrowIfCancellationRequested();

        //                         // Generating Data pack
        //                         failCode = await DataPack.FromBeatSaberData(tempUnZipPath, datapackOutputPath, packInfo, beatMapSongList, cancellationToken);
        //                         if (failCode >= 0)
        //                             return failCode;
        //                     }
        //                     else
        //                         return 2;

        //                 }
        //                 catch (OperationCanceledException)
        //                 {
        //                     SafeFileManagement.DeleteDirectory(tempUnZipPath);
        //                 }
        //                 catch (ObjectDisposedException)
        //                 {
        //                     SafeFileManagement.DeleteDirectory(tempUnZipPath);
        //                 }

        //                 // Successfully converted map
        //                 SafeFileManagement.DeleteDirectory(tempUnZipPath);
        //                 return -1;
        //             }
        //             return 1;
        //         }
        //         return 0;
        //     });
        // }

        // /// <summary>
        // /// Unzip the beatsaber map pack into a temp directory
        // /// </summary>
        // /// <param name="fileToUnzip">path of file to unzip</param>
        // /// <param name="pathToUnzip">path to unzip the file into</param>
        // /// <param name="cancellationToken">Token that allows async function to be canceled</param>
        // /// <returns>the location of the unzipped file</returns>
        // public static Task<string> UnZipFileAsync(string fileToUnzip, string pathToUnzip, CancellationToken cancellationToken)
        // {
        //     return Task.Run(async () =>
        //     {
        //         string tempUnZipPath = Path.Combine(pathToUnzip, SafeFileManagement.GetFolderName(fileToUnzip));
        //         if (Directory.Exists(tempUnZipPath))
        //             SafeFileManagement.DeleteDirectory(tempUnZipPath);

        //         Directory.CreateDirectory(tempUnZipPath);
        //         await Archive.DecompressAsync(fileToUnzip, tempUnZipPath, cancellationToken);
        //         return tempUnZipPath;
        //     });
        // }

        // /// <summary>
        // /// Convert egg file to ogg
        // /// </summary>
        // /// <param name="rootFilePath">Folder that contains .egg files</param>
        // /// <param name="packInfo">Info object that contains data about beatsaber songs</param>
        // /// <returns>Updated pack info object</returns>
        // public static Info ConvertSoundFile(string rootFilePath, Info packInfo)
        // {
        //     string[] files = Directory.GetFiles(rootFilePath, "*.egg*", SearchOption.AllDirectories);
        //     string[] alreadyConvertedfiles = Directory.GetFiles(rootFilePath, "*.ogg*", SearchOption.AllDirectories);
        //     packInfo._songFilename = packInfo._songFilename.Replace(".egg", ".ogg");

        //     if (files.Length == 0 && alreadyConvertedfiles.Length == 0)
        //         return null;

        //     foreach (string path in files)
        //     {
        //         string newName = path.Replace(".egg", ".ogg");
        //         SafeFileManagement.MoveFile(path, newName);
        //     }
        //     return packInfo;
        // }

        // /// <summary>
        // /// Convert images file to png
        // /// </summary>
        // /// <param name="rootFilePath">Folder that contains .jpg files</param>
        // /// <param name="packInfo">info object that contains data about beatsaber songs</param>
        // /// <returns>updated pack info object</returns>
        // public static Info ConvertImageFiles(string rootFilePath, Info packInfo)
        // {
        //     string[] files = Directory.GetFiles(rootFilePath, "*.jpg*", SearchOption.AllDirectories);
        //     packInfo._coverImageFilename = packInfo._coverImageFilename.Replace(".jpg", ".png");

        //     foreach (string path in files)
        //     {
        //         string newName = path.Replace(".jpg", ".png");
        //         SafeFileManagement.MoveFile(path, newName);
        //     }
        //     return packInfo;
        // }

        // /// <summary>
        // /// Read dat files as json files and load into struct
        // /// </summary>
        // /// <param name="rootFilePath">Folder that contains beatmap files</param>
        // /// <param name="packInfo">info object that contains data about beatsaber songs</param>
        // /// <returns>List of parsed beatmap data</returns>
        // public static Task<List<BeatMapSong>> ParseBeatSaberDat(string rootFilePath, Info packInfo)
        // {
        //     return Task.Run(() =>
        //     {
        //         List<BeatMapSong> beatMapSongList = new List<BeatMapSong>();

        //         foreach (DifficultyBeatmapSets difficultyBeatmapSets in packInfo._difficultyBeatmapSets)
        //         {
        //             foreach (DifficultyBeatmap difficultyBeatmaps in difficultyBeatmapSets.DifficultyBeatmaps)
        //             {
        //                 string songData = Path.Combine(rootFilePath, difficultyBeatmaps._beatmapFilename);
        //                 BeatMapData beatMapData = JsonUtility.FromJson<BeatMapData>(SafeFileManagement.GetFileContents(songData));
        //                 beatMapSongList.Add(new BeatMapSong(beatMapData, difficultyBeatmaps, difficultyBeatmapSets.BeatmapCharacteristicName));
        //             }
        //         }
        //         return beatMapSongList;
        //     });
        // }

        // /// <summary>
        // /// Return PackInfo from within a root folder
        // /// </summary>
        // /// <param name="rootFilePath">folder that contains Info.dat</param>
        // /// <returns>Parsed info from Info.dat</returns>
        // public static Info GetPackInfo(string rootFilePath)
        // {
        //     string infoPath = Path.Combine(rootFilePath, "Info.dat");
        //     return JsonUtility.FromJson<Info>(SafeFileManagement.GetFileContents(infoPath));
        // }
    }
}
