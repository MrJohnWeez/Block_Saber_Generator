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
    public static class ConvertZip
    {
        /// <summary>
        /// Convert a Beat Saber Zip file into a Minecraft Resourcepack and Datapack
        /// </summary>
        /// <param name="zipPath">file path to beat saber zip file</param>
        /// <param name="datapackOutputPath">folder path that minecraft zips will be generated</param>
        /// <param name="uuid">Unique number that determines song value</param>
        /// <param name="cancellationToken">Token that allows async function to be canceled</param>
        /// <returns>-1 on Sucess</returns>
        public static Task<int> ConvertAsync(string zipPath, string datapackOutputPath, int uuid, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                if (File.Exists(zipPath) && Directory.Exists(datapackOutputPath))
                {
                    // Decompressing files
                    string tempUnZipPath = await UnZipFileAsync(zipPath, ProcessManager.temporaryPath, cancellationToken);

                    // Parsing files
                    Info packInfo = GetPackInfo(tempUnZipPath);
                    if (packInfo != null)
                    {
                        try
                        {
                            packInfo._uuid = uuid;
                            List<BeatMapSong> beatMapSongList = await ParseBeatSaberDat(tempUnZipPath, packInfo);


                            // Converting files
                            packInfo = ConvertSoundFile(tempUnZipPath, packInfo);
                            if (packInfo == null)
                                return 4;

                            packInfo = ConvertImageFiles(tempUnZipPath, packInfo);

                            cancellationToken.ThrowIfCancellationRequested();

                            if (beatMapSongList.Count > 0 && packInfo != null)
                            {
                                // Generating Resource pack
                                int failCode = await ResourcePack.FromBeatSaberData(tempUnZipPath, datapackOutputPath, packInfo);
                                if (failCode >= 0)
                                    return failCode;

                                cancellationToken.ThrowIfCancellationRequested();

                                // Generating Data pack
                                failCode = await DataPack.FromBeatSaberData(tempUnZipPath, datapackOutputPath, packInfo, beatMapSongList, cancellationToken);
                                if (failCode >= 0)
                                    return failCode;
                            }
                            else
                                return 2;

                        }
                        catch (OperationCanceledException)
                        {
                            SafeFileManagement.DeleteDirectory(tempUnZipPath);
                        }
                        catch (ObjectDisposedException)
                        {
                            SafeFileManagement.DeleteDirectory(tempUnZipPath);
                        }

                        // Successfully converted map
                        SafeFileManagement.DeleteDirectory(tempUnZipPath);
                        return -1;
                    }
                    return 1;
                }
                return 0;
            });
        }

        public static BeatSaberMap ConvertFilesEggToOgg(BeatSaberMap beatSaberMap)
        {
            string[] files = Directory.GetFiles(beatSaberMap.ExtractedFilePath, "*.egg*", SearchOption.AllDirectories);
            string[] alreadyConvertedfiles = Directory.GetFiles(beatSaberMap.ExtractedFilePath, "*.ogg*", SearchOption.AllDirectories);
            if (files.Length == 0 && alreadyConvertedfiles.Length == 0)
            {
                return beatSaberMap;
            }
            beatSaberMap.Info.SongFilename = beatSaberMap.Info.SongFilename.Replace(".egg", ".ogg");
            foreach (string path in files)
            {
                string newName = path.Replace(".egg", ".ogg");
                SafeFileManagement.MoveFile(path, newName);
            }
            return beatSaberMap;
        }

        public static BeatSaberMap ConvertFilesJpgToPng(BeatSaberMap beatSaberMap)
        {
            string[] files = Directory.GetFiles(beatSaberMap.ExtractedFilePath, "*.jpg*", SearchOption.AllDirectories);
            beatSaberMap.Info.CoverImageFilename = beatSaberMap.Info.CoverImageFilename.Replace(".jpg", ".png");
            foreach (string path in files)
            {
                string newName = path.Replace(".jpg", ".png");
                SafeFileManagement.MoveFile(path, newName);
            }
            return beatSaberMap;
        }
    }
}
