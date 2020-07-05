// Created by MrJohnWeez
// June 2020

using Minecraft;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Minecraft
{
	/// <summary>
	/// Static class for commonly used values
	/// </summary>
	public static class Globals
	{
		public const int C_numberOfIORetryAttempts = 5;
		public const int C_CommandLimit = 10000;

		// Extension names
		public static readonly string[] excludeExtensions = { ".meta" };
		public static readonly string[] excludeKeyVarExtensions = { ".meta", ".nbt" };
		public const string C_Zip = ".zip";
		public const string C_Ogg = ".ogg";
		public const string C_McFunction = ".mcfunction";

		// File Names
		public const string C_PackMeta = "pack.mcmeta";
		public const string C_PackIcon = "pack.png";
		public const string C_SoundsJson = "sounds.json";
		public const string C_SpawnNotesBaseFunction = "spawn_notes_base.mcfunction";
		public const string C_InitFunction = "init.mcfunction";
		public const string C_Difficulties = "difficulties.mcfunction";
		public const string C_SetSpawnOrgin = "set_spawn_orgin.mcfunction";
		public const string C_TemplateStrings = "TemplateStrings.json";
		public const string C_MapDifficultyCompleted = "map_difficulty_completed.mcfunction";

		// Sub words
		public const string C_LvlNoteName = "_lvl_note_";
		public const string C_LvlObsicleName = "_lvl_obsicle_";

		// Folder Names
		public const string C_Minecraft = "minecraft";
		public const string C_TemplateResourcePackName = @"SONG_AUTHOR - SONG_NAME";
		public const string C_TemplateDataPackName = "BlockSaberTemplate";
		public const string C_Assets = "assets";
		public const string C_Sounds = "sounds";
		public const string C_Custom = "custom";
		public const string C_ResourcePack = "ResourcePack_";
		public const string C_Data = "data";
		public const string C_Functions = "functions";
		public const string C_Datapack = "DataPack_";
		public const string C_BlockSaberBase = "_root_class";
		public const string C_FolderUUID = "folder_uuid";
		public static readonly string pathOfResourcepackTemplate = Path.Combine(ProcessManager.streamingAssets, "TemplateResourcepack");
		public static readonly string pathOfDatapackTemplate = Path.Combine(ProcessManager.streamingAssets, "TemplateDatapack");
		public static readonly string pathOfTemplateStrings = Path.Combine(ProcessManager.streamingAssets, C_TemplateStrings);
		public static readonly TemplateStrings templateStrings = JsonUtility.FromJson<TemplateStrings>(SafeFileManagement.GetFileContents(pathOfTemplateStrings));

		// Beat saber data
		public static readonly string[][] noteTypes = new string[4][] {new string[9]
																	{
																		"red_up",
																		"red_down",
																		"red_left",
																		"red_right",
																		"red_up_left",
																		"red_up_right",
																		"red_down_left",
																		"red_down_right",
																		"red_dot"
																	},
																	new string[9]
																	{
																		"blue_up",
																		"blue_down",
																		"blue_left",
																		"blue_right",
																		"blue_up_left",
																		"blue_up_right",
																		"blue_down_left",
																		"blue_down_right",
																		"blue_dot"
																	},
																	new string[9],
																	new string[9]
																	{"bomb","bomb","bomb","bomb","bomb","bomb","bomb","bomb","bomb"}};

	}
}