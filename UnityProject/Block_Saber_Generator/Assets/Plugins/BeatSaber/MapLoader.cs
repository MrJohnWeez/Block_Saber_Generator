using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BeatSaber.Data;
using UnityEngine;
using Utilities.Wrappers;

namespace BeatSaber
{
    public static class MapLoader
    {
        /// <summary>
        /// Get all data from beatsaber map zip file
        /// </summary>
        /// <param name="fileToUnzip">path to beat saber map zip</param>
        /// <param name="pathToUnzip">path to unzip map data</param>
        /// <param name="cancellationToken">token to cancel action</param>
        /// <returns></returns>
        public static async Task<BeatSaberMap> GetDataFromMapZip(string fileToUnzip, string pathToUnzip, CancellationToken cancellationToken)
        {
            string tempUnZipPath = Path.Combine(pathToUnzip, "MapLoader", SafeFileManagement.GetFolderName(fileToUnzip));
            if (Directory.Exists(tempUnZipPath))
            {
                SafeFileManagement.DeleteDirectory(tempUnZipPath);
            }
            Directory.CreateDirectory(tempUnZipPath);
            await Archive.DecompressAsync(fileToUnzip, tempUnZipPath, cancellationToken);

            string infoPath = Path.Combine(tempUnZipPath, "info.dat");
            Info info = JsonUtility.FromJson<Info>(SafeFileManagement.GetFileContents(infoPath));
            if (info == null) { return null; }
            info = ConvertSoundFile(tempUnZipPath, info);
            if (info == null) { return null; }
            info = ConvertImageFiles(tempUnZipPath, info);
            if (info == null) { return null; }

            Dictionary<string, MapDataInfo> mapDataInfos = new Dictionary<string, MapDataInfo>();
            foreach (var beatMapSets in info.DifficultyBeatmapSets)
            {
                foreach (var beatMap in beatMapSets.DifficultyBeatmaps)
                {
                    string mapPath = Path.Combine(tempUnZipPath, beatMap.BeatmapFilename);
                    MapData mapData = JsonUtility.FromJson<MapData>(SafeFileManagement.GetFileContents(mapPath));
                    mapDataInfos.Add(beatMap.BeatmapFilename, new MapDataInfo(beatMap, mapData));
                }
            }
            return new BeatSaberMap(info, mapDataInfos, tempUnZipPath);
        }

        /// <summary>
        /// Convert egg file to ogg
        /// </summary>
        /// <param name="rootFilePath">Folder that contains .egg files</param>
        /// <param name="info">Info object that contains data about beatsaber songs</param>
        /// <returns>Updated pack info object</returns>
        public static Info ConvertSoundFile(string rootFilePath, Info info)
        {
            string[] files = Directory.GetFiles(rootFilePath, "*.egg*", SearchOption.AllDirectories);
            string[] alreadyConvertedfiles = Directory.GetFiles(rootFilePath, "*.ogg*", SearchOption.AllDirectories);
            if (string.IsNullOrEmpty(info.SongFilename))
            {
                return null;
            }
            info.SongFilename = info.SongFilename.Replace(".egg", ".ogg");
            if (files.Length == 0 && alreadyConvertedfiles.Length == 0)
            {
                return null;
            }
            foreach (string path in files)
            {
                string newName = path.Replace(".egg", ".ogg");
                SafeFileManagement.MoveFile(path, newName);
            }
            return info;
        }

        /// <summary>
        /// Convert images file to png
        /// </summary>
        /// <param name="rootFilePath">Folder that contains .jpg files</param>
        /// <param name="packInfo">info object that contains data about beatsaber songs</param>
        /// <returns>updated pack info object</returns>
        public static Info ConvertImageFiles(string rootFilePath, Info packInfo)
        {
            string[] files = Directory.GetFiles(rootFilePath, "*.jpg*", SearchOption.AllDirectories);
            if (string.IsNullOrEmpty(packInfo.CoverImageFilename))
            {
                return null;
            }
            packInfo.CoverImageFilename = packInfo.CoverImageFilename.Replace(".jpg", ".png");
            foreach (string path in files)
            {
                string newName = path.Replace(".jpg", ".png");
                SafeFileManagement.MoveFile(path, newName);
            }
            return packInfo;
        }
    }
}
