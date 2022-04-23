using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BeatSaber;
using Minecraft.Generator;
using MJW.Conversion;
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
        /// <returns>-1 on Success</returns>
        public static async Task<ConversionError> ConvertAsync(string zipPath, string datapackOutputPath, int uuid, IProgress<ConversionProgress> progress, CancellationToken cancellationToken)
        {
            if (!File.Exists(zipPath) || !Directory.Exists(datapackOutputPath))
            {
                return ConversionError.MissingInfo;
            }
            progress.Report(new ConversionProgress(0.1f, "Loading beat map file"));
            var beatSaberMap = await MapLoader.GetDataFromMapZip(zipPath, ProcessManager.temporaryPath, cancellationToken);
            if (beatSaberMap == null)
            {
                return ConversionError.InvalidBeatMap;
            }
            cancellationToken.ThrowIfCancellationRequested();
            var tempFolder = beatSaberMap.ExtractedFilePath;
            try
            {
                beatSaberMap = ConvertFilesEggToOgg(beatSaberMap);
                beatSaberMap = ConvertFilesJpgToPng(beatSaberMap);
                if (beatSaberMap.InfoData.DifficultyBeatmapSets.Length == 0)
                {
                    return ConversionError.NoMapData;
                }
                cancellationToken.ThrowIfCancellationRequested();
                // Generating Resource pack
                progress.Report(new ConversionProgress(0.2f, "Generating resource pack"));
                var resourcepackError = await ResourcePack.FromBeatSaberData(datapackOutputPath, beatSaberMap);
                if (resourcepackError != ConversionError.None)
                {
                    return resourcepackError;
                }
                cancellationToken.ThrowIfCancellationRequested();
                // Generating Data pack
                progress.Report(new ConversionProgress(0.3f, "Generating datapack"));
                var datapackError = await DataPack.FromBeatSaberData(datapackOutputPath, beatSaberMap, progress, cancellationToken);
                if (datapackError != ConversionError.None)
                {
                    return datapackError;
                }
            }
            catch (OperationCanceledException e)
            {
                SafeFileManagement.DeleteDirectory(tempFolder);
                throw (e);
            }
            catch (ObjectDisposedException)
            {
                SafeFileManagement.DeleteDirectory(tempFolder);
            }

            // Successfully converted map
            SafeFileManagement.DeleteDirectory(tempFolder);
            return ConversionError.None;
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
