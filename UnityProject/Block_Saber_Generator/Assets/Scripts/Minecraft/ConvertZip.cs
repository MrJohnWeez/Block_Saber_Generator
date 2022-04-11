using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BeatSaber;
using Minecraft.Generator;
using UnityEngine;
using Utilities.Wrappers;

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
                    var beatSaberMap = await MapLoader.GetDataFromMapZip(zipPath, ProcessManager.temporaryPath, cancellationToken);
                    var tempFolder = beatSaberMap.ExtractedFilePath;
                    if (beatSaberMap != null)
                    {
                        try
                        {
                            beatSaberMap = ConvertFilesEggToOgg(beatSaberMap);
                            beatSaberMap = ConvertFilesJpgToPng(beatSaberMap);
                            cancellationToken.ThrowIfCancellationRequested();
                            if (beatSaberMap.InfoData.DifficultyBeatmapSets.Length > 0)
                            {
                                // Generating Resource pack
                                int failCode = await ResourcePack.FromBeatSaberData(datapackOutputPath, beatSaberMap);
                                if (failCode >= 0)
                                {
                                    return failCode;
                                }
                                cancellationToken.ThrowIfCancellationRequested();
                                // Generating Data pack
                                failCode = await DataPack.FromBeatSaberData(datapackOutputPath, beatSaberMap, cancellationToken);
                                if (failCode >= 0)
                                {
                                    return failCode;
                                }
                            }
                            else
                            {
                                return 2;
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            SafeFileManagement.DeleteDirectory(tempFolder);
                        }
                        catch (ObjectDisposedException)
                        {
                            SafeFileManagement.DeleteDirectory(tempFolder);
                        }

                        // Successfully converted map
                        SafeFileManagement.DeleteDirectory(tempFolder);
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
            beatSaberMap.InfoData.SongFilename = beatSaberMap.InfoData.SongFilename.Replace(".egg", ".ogg");
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
            beatSaberMap.InfoData.CoverImageFilename = beatSaberMap.InfoData.CoverImageFilename.Replace(".jpg", ".png");
            foreach (string path in files)
            {
                string newName = path.Replace(".jpg", ".png");
                SafeFileManagement.MoveFile(path, newName);
            }
            return beatSaberMap;
        }
    }
}
