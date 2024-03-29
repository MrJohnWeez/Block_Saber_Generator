﻿using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BeatSaber;
using MJW.Conversion;
using Utilities;
using Utilities.Wrappers;

namespace Minecraft.Generator
{
    /// <summary>
    /// Class that allows for quick conversion of Beat Saber data to a Minecraft resourcepack
    /// </summary>
    public static class ResourcePack
    {
        /// <summary>
        /// Convert Beat Saber data to a minecraft ResourcePack
        /// </summary>
        /// <param name="unzippedFolderPath">Path to unzipped beat saber pack</param>
        /// <param name="datapackOutputPath">Folder path that Resourcepack will be generated in</param>
        /// <param name="packInfo">Beat Saber Infomation</param>
        /// <returns>-1 if successful</returns>
        public static async Task<ConversionError> FromBeatSaberData(string datapackOutputPath, BeatSaberMap beatSaberMap)
        {
            return await Task.Run(() =>
            {
                var packInfo = beatSaberMap.InfoData;
                var unzippedFolderPath = beatSaberMap.ExtractedFilePath;
                if (!Directory.Exists(unzippedFolderPath) || packInfo == null)
                {
                    return ConversionError.MissingInfo;
                }

                Dictionary<string, string> keyVars = new Dictionary<string, string>();

                string folder_uuid = SafeFileManagement.GetFileName(Path.GetFileName(unzippedFolderPath)).MakeMinecraftSafe();
                string packName = Globals.RESOURCEPACK + folder_uuid;

                // Paths
                string fullOutputPath = Path.Combine(datapackOutputPath, packName + Globals.ZIP);
                string rootFolderPath = Path.Combine(unzippedFolderPath, packName);
                string minecraftNamespace = Path.Combine(rootFolderPath, Globals.ASSETS, Globals.MINECRAFT);
                string mapSong = Path.Combine(unzippedFolderPath, packInfo.SongFilename);
                string packSong = Path.Combine(minecraftNamespace, Globals.SOUNDS, Globals.CUSTOM, folder_uuid + Globals.OGG);
                string mapIcon = Path.Combine(unzippedFolderPath, packInfo.CoverImageFilename);
                string packIcon = Path.Combine(rootFolderPath, Globals.PACK_ICON);

                // Replaced vars
                keyVars["SONGUUID"] = folder_uuid;
                keyVars["SONGNAME"] = packInfo.SongName + packInfo.SongSubName;
                keyVars["AUTHORNAME"] = packInfo.SongAuthorName;

                // Copying Template
                string copiedTemplatePath = Path.Combine(unzippedFolderPath, Globals.TEMPLATE_RESOURCES_PACK_NAME);

                if (SafeFileManagement.DirectoryCopy(Globals.pathOfResourcepackTemplate, unzippedFolderPath, true, Globals.excludeExtensions, Globals.NUMBER_OF_IO_RETRY_ATTEMPTS))
                {
                    if (SafeFileManagement.MoveDirectory(copiedTemplatePath, rootFolderPath, Globals.NUMBER_OF_IO_RETRY_ATTEMPTS))
                    {
                        Filemanagement.UpdateAllCopiedFiles(rootFolderPath, keyVars);

                        // Copying Image Icon
                        SafeFileManagement.CopyFileTo(mapIcon, packIcon, true, Globals.NUMBER_OF_IO_RETRY_ATTEMPTS);

                        // Copying Song
                        if (SafeFileManagement.CopyFileTo(mapSong, packSong, true, Globals.NUMBER_OF_IO_RETRY_ATTEMPTS))
                        {
                            if (!Filemanagement.UpdateFileWithKeys(Path.Combine(minecraftNamespace, Globals.SOUNDS_JSON), keyVars))
                            {
                                return ConversionError.OtherFail;
                            }
                        }

                        // Creating Zip
                        Archive.Compress(rootFolderPath, fullOutputPath, true);
                        return ConversionError.None;
                    }
                }
                return ConversionError.FailedToCopyFile;
            });
        }
    }
}

