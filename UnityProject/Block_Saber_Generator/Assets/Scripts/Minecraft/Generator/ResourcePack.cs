﻿// Created by MrJohnWeez
// June 2020

using BeatSaber.packInfo;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MrJohnWeez.Extensions;
using System.Threading;
using System.Threading.Tasks;

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
		/// <param name="cancellationToken">Token that allows async function to be canceled</param>
		/// <returns>-1 if successful</returns>
		public static Task<int> FromBeatSaberData(string unzippedFolderPath, string datapackOutputPath, PackInfo packInfo)
		{
			return Task.Run(() =>
			{
				// Validate inputs
				if (!Directory.Exists(unzippedFolderPath) || packInfo == null)
					return 0;

				Dictionary<string, string> keyVars = new Dictionary<string, string>();

				string folder_uuid = SafeFileManagement.GetFileName(Path.GetFileName(unzippedFolderPath)).MakeMinecraftSafe();
				string packName = Globals.C_ResourcePack + folder_uuid;

				// Paths
				string fullOutputPath = Path.Combine(datapackOutputPath, packName + Globals.C_Zip);
				string rootFolderPath = Path.Combine(unzippedFolderPath, packName);
				string minecraftNamespace = Path.Combine(rootFolderPath, Globals.C_Assets, Globals.C_Minecraft);
				string mapSong = Path.Combine(unzippedFolderPath, packInfo._songFilename);
				string packSong = Path.Combine(minecraftNamespace, Globals.C_Sounds, Globals.C_Custom, folder_uuid + Globals.C_Ogg);
				string mapIcon = Path.Combine(unzippedFolderPath, packInfo._coverImageFilename);
				string packIcon = Path.Combine(rootFolderPath, Globals.C_PackIcon);

				// Replaced vars
				keyVars["SONGUUID"] = folder_uuid;
				keyVars["SONGNAME"] = packInfo._songName + packInfo._songSubName;
				keyVars["AUTHORNAME"] = packInfo._songAuthorName;

				// Copying Template
				string copiedTemplatePath = Path.Combine(unzippedFolderPath, Globals.C_TemplateResourcePackName);


				if (SafeFileManagement.DirectoryCopy(Globals.pathOfResourcepackTemplate, unzippedFolderPath, true, Globals.excludeExtensions, Globals.C_numberOfIORetryAttempts))
				{
					if (SafeFileManagement.MoveDirectory(copiedTemplatePath, rootFolderPath, Globals.C_numberOfIORetryAttempts))
					{
						Filemanagement.UpdateAllCopiedFiles(rootFolderPath, keyVars);

						// Copying Image Icon
						SafeFileManagement.CopyFileTo(mapIcon, packIcon, true, Globals.C_numberOfIORetryAttempts);

						// Copying Song
						if (SafeFileManagement.CopyFileTo(mapSong, packSong, true, Globals.C_numberOfIORetryAttempts))
						{
							Filemanagement.UpdateFileWithKeys(Path.Combine(minecraftNamespace, Globals.C_SoundsJson), keyVars);
						}

						// Creating Zip
						Archive.Compress(rootFolderPath, fullOutputPath);
						return -1;
					}
				}
				return 0;
			});
		}
	}
}

