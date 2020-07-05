// Created by MrJohnWeez
// June 2020

using BeatSaber;
using BeatSaber.packInfo;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using MrJohnWeez.Extensions;

namespace Minecraft.Generator
{
	/// <summary>
	/// Stores data for datapack Generation
	/// </summary>
	public class DataPackData
	{
		public Dictionary<string, string> keyVars;

		// Names
		public string folder_uuid;
		public string packName;
		public string UUIDVar;

		// Paths
		public string datapackRootPath;
		public string fullOutputPath;
		public string blockSaberBaseFunctionsPath;
		public string folder_uuidFunctionsPath;
		public string spawnNotesBasePath;

		// Values
		public double metersPerTick;
		public double ticksStartOffset;

		public DataPackData(string unzippedFolderPath, string datapackOutputPath, PackInfo packInfo, List<BeatMapSong> beatMapSongList)
		{
			// Names
			keyVars = new Dictionary<string, string>();
			folder_uuid = SafeFileManagement.GetFileName(Path.GetFileName(unzippedFolderPath)).MakeMinecraftSafe();
			packName = Globals.C_Datapack + folder_uuid;
			UUIDVar = UnityEngine.Random.Range(-999999999, 999999999).ToString("X");

			// Paths
			datapackRootPath = Path.Combine(unzippedFolderPath, packName);
			fullOutputPath = Path.Combine(datapackOutputPath, packName + Globals.C_Zip);
			blockSaberBaseFunctionsPath = Path.Combine(datapackRootPath, Globals.C_Data, Globals.C_BlockSaberBase, Globals.C_Functions);
			folder_uuidFunctionsPath = Path.Combine(datapackRootPath, Globals.C_Data, folder_uuid, Globals.C_Functions);
			spawnNotesBasePath = Path.Combine(folder_uuidFunctionsPath, Globals.C_SpawnNotesBaseFunction);

			// Values
			metersPerTick = packInfo._beatsPerMinute / 60.0d * 24 * 0.21 / 20;
			ticksStartOffset = (int)(Mathf.Clamp((float)(packInfo._beatsPerMinute / 60d * 10), 7, 20) / metersPerTick);

			// Set up Keys
			keyVars["MAPPER_NAME"] = packInfo._levelAuthorName;
			keyVars["BEATS_PER_MINUTE"] = packInfo._beatsPerMinute.ToString();
			keyVars["SONGID"] = ((int)Random.Range(-999999999, 999999999)).ToString();
			keyVars["MOVESPEED"] = metersPerTick.ToString();
			keyVars["SONGTITLE"] = packInfo._songName + " " + packInfo._songSubName;
			keyVars["SONGSHORTNAME"] = packInfo._songName;
			keyVars["SONGARTIST"] = packInfo._songAuthorName;
			keyVars["folder_uuid"] = folder_uuid;

			string listOfDifficulties = "";
			for (int diffNumber = 0; diffNumber < beatMapSongList.Count; diffNumber++)
			{
				listOfDifficulties += beatMapSongList[diffNumber].difficultyBeatmaps._difficulty;
				if (diffNumber < beatMapSongList.Count - 1)
					listOfDifficulties += " | ";
			}
			keyVars["DIFFICULTYLIST"] = listOfDifficulties;
		}
	}
}
