using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DatapackGenerator : GeneratorBase
{
	private const int C_CommandLimit = 10000;

	private readonly string[][] _noteTypes = new string[4][] {new string[9] 
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
																	{"","","","","","","","","bomb"}};

	// Extension names
	private const string C_McFunction = ".mcfunction";

	// Folder Names
	private const string C_Data = "data";
	private const string C_Functions = "functions";
	private const string C_Datapack = "DataPack_";
	private const string C_TemplateName = "BlockSaberTemplate";
	private const string C_BlockSaberBase = "_block_saber_base";
	private const string C_FolderUUID = "folder_uuid";

	// File names
	private const string C_SpawnNotesBaseFunction = "spawn_notes_base.mcfunction";
	private const string C_InitFunction = "init.mcfunction";
	private const string C_Difficulties = "difficulties.mcfunction";
	private const string C_TemplateStrings = "TemplateStrings.json";

	// Part names
	private const string C_LvlName = "_lvl_";

	// Paths
	private string _datapackRootPath = "";
	private string _blockSaberBaseFunctionsPath = "";
	private string _spawnNotesBasePath = "";
	private string _folder_uuidFunctionsPath = "";

	private string _pathOfDatapackTemplate = Path.Combine(C_StreamingAssets, "TemplateDatapack");

	private List<BeatMapSong> _beatMapSongList;

	private double _metersPerTick = 0;
	private string _UUIDHex = "";
	private TemplateStrings _templateStrings = null;
	private double _offset = 0.0;

	public DatapackGenerator(string unzippedFolderPath, PackInfo packInfo, List<BeatMapSong> beatMapSongList, string datapackOutputPath)
	{
		this._unzippedFolderPath = unzippedFolderPath;
		this._packInfo = packInfo;
		this._beatMapSongList = beatMapSongList;
		this._outputPath = datapackOutputPath;
		Init();
	}

	/// <summary>
	/// Generate datapack
	/// </summary>
	/// <returns></returns>
	public override bool Generate()
	{

		




		if (Directory.Exists(_unzippedFolderPath) && _packInfo != null && _beatMapSongList.Count > 0)
		{
			Debug.Log("Copying Template...");
			if (CopyTemplate(_pathOfDatapackTemplate, _unzippedFolderPath))
			{
				UpdateAllCopiedFiles(_datapackRootPath, true);

				Debug.Log("Generating main datapack files...");
				GenerateMCBeatData();

				Debug.Log("Zipping files...");
				CreateArchive(_datapackRootPath, _fullOutputPath);

				Debug.Log("Datapack Done");
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Copy the Datapack template and rename
	/// </summary>
	/// <param name="sourceDirName">Source folder path to copy</param>
	/// <param name="destDirName">Path to copy to</param>
	/// <returns></returns>
	protected override bool CopyTemplate(string sourceDirName, string destDirName)
	{
		if (base.CopyTemplate(sourceDirName, destDirName))
		{
			string copiedTemplatePath = Path.Combine(destDirName, C_TemplateName);
			_rootFolderPath = Path.Combine(destDirName, _packName);
			return SafeFileManagement.MoveDirectory(copiedTemplatePath, _rootFolderPath, C_numberOfIORetryAttempts);
		}
		return false;
	}

	/// <summary>
	/// Set up varibles and paths based on class data
	/// </summary>
	protected override void Init()
	{
		base.Init();
		// minecraft data
		string _pathOfTemplateStrings = Path.Combine(C_StreamingAssets, C_TemplateStrings);
		_templateStrings = JsonUtility.FromJson<TemplateStrings>(SafeFileManagement.GetFileContents(_pathOfTemplateStrings));
		
		// Names
		_folder_uuid = SafeFileManagement.GetFileName(Path.GetFileName(_unzippedFolderPath)).MakeMinecraftSafe();
		_packName = C_Datapack + _folder_uuid;
		_UUIDHex = Random.Range(-999999999, 999999999).ToString("X");

		// Paths
		_datapackRootPath = Path.Combine(_unzippedFolderPath, _packName);
		_fullOutputPath = Path.Combine(_outputPath, _packName + C_Zip);
		_blockSaberBaseFunctionsPath = Path.Combine(_datapackRootPath, C_Data, C_BlockSaberBase, C_Functions);
		_folder_uuidFunctionsPath = Path.Combine(_datapackRootPath, C_Data, _folder_uuid, C_Functions);
		_spawnNotesBasePath = Path.Combine(_folder_uuidFunctionsPath, C_SpawnNotesBaseFunction);

		// Values
		_metersPerTick = _packInfo._beatsPerMinute  / 60.0d * 24 * 0.21 / 20;
		_offset = _metersPerTick * -21;

		// Keys
		_keyVars["MAPPER_NAME"] = _packInfo._levelAuthorName;
		_keyVars["BEATS_PER_MINUTE"] = _packInfo._beatsPerMinute.ToString();
		_keyVars["SONGID"] = Random.Range(-999999999, 999999999).ToString();
		_keyVars["MOVESPEED"] = _metersPerTick.ToString();
		_keyVars["SONGTITLE"] = _packInfo._songName + " " + _packInfo._songSubName;
		_keyVars["SONGARTIST"] = _packInfo._songAuthorName;
		_keyVars["OFFSET"] = string.Format("{0:F18}", _offset);
		_offset = double.Parse(_keyVars["OFFSET"]);
		_keyVars["SONG"] = _folder_uuid;
		_keyVars["folder_uuid"] = _folder_uuid;
	}

	/// <summary>
	/// Update all files within a directory to correct varible names
	/// </summary>
	/// <param name="folderPath">In folder path</param>
	private void UpdateAllCopiedFiles(string folderPath, bool checkSubDirectories = false)
	{
		// Must change the folder names before searching for keys
		string songname_uuidFolder = Path.Combine(_datapackRootPath, C_Data, C_FolderUUID);
		string newPath = Path.Combine(_datapackRootPath, C_Data, _folder_uuid);
		SafeFileManagement.MoveDirectory(songname_uuidFolder, newPath, C_numberOfIORetryAttempts);

		if (checkSubDirectories)
		{
			string[] dirs = SafeFileManagement.GetDirectoryPaths(folderPath, C_numberOfIORetryAttempts);
			foreach (string dir in dirs)
			{
				UpdateAllCopiedFiles(dir, checkSubDirectories);
			}
		}

		if (Directory.Exists(folderPath))
		{
			string[] files = SafeFileManagement.GetFilesPaths(folderPath, C_numberOfIORetryAttempts);
			foreach (string file in files)
			{
				UpdateFileWithKeys(file);
			}
		}
	}

	/// <summary>
	/// Main generation of minecraft commands for beat saber data
	/// </summary>
	private void GenerateMCBeatData()
	{
		string difficultiesFunctionPath = Path.Combine(_folder_uuidFunctionsPath, C_Difficulties);
		string initFunctionPath = Path.Combine(_blockSaberBaseFunctionsPath, C_InitFunction);
		int difficultyID = 1;
		double prevNodeTime = 0;
		int nodeRowID = 1;
		int difficulty = 0;
		
		foreach (BeatMapSong song in _beatMapSongList)
		{
			string difficultyName = song.difficultyBeatmaps._difficulty.MakeMinecraftSafe();
			Debug.Log("Generating song for mode: " + difficultyName);

			difficulty++;
			string spawnNotesBaseCommand = string.Format("execute if score @s Difficulty matches {0} as @s run function {1}:{2}",
														difficulty,
														_folder_uuid,
														difficultyName);
			SafeFileManagement.AppendFile(_spawnNotesBasePath, spawnNotesBaseCommand);

			string commandBasePath = Path.Combine(_folder_uuidFunctionsPath, difficultyName + C_McFunction);

			int currentLevel = 0;
			int currentTick = 0;
			int prevCurrentTick = 0;
			int currentNumberOfCommands = 0;
			int noteIndex = 0;
			int obsicleIndex = 0;
			string modeID = _UUIDHex + difficultyID.ToString();
			

			string scoreboardCommand = string.Format("scoreboard objectives add {0} dummy{1}execute as @a unless score @s {0} = @s {0} run scoreboard players set @s {0} 0",
													modeID,
													System.Environment.NewLine);

			SafeFileManagement.AppendFile(initFunctionPath, scoreboardCommand);
			


			string difficultyDisplayCommands = CreateDifficultyDisplay(modeID, difficultyName, _folder_uuid);
			SafeFileManagement.AppendFile(difficultiesFunctionPath, difficultyDisplayCommands);



			string playCommands = string.Format("scoreboard players set @s Difficulty {0}{1}execute as @s run function {2}:play",
												difficultyID,
												System.Environment.NewLine,
												_folder_uuid);
			string playPath = Path.Combine(_folder_uuidFunctionsPath, difficultyName + "_play" + C_McFunction);
			SafeFileManagement.SetFileContents(playPath, playCommands);


			_notes[] notes = song.beatMapData._notes;
			_obstacles[] obstacles = song.beatMapData._obstacles;
			int currentCommandLimit = C_CommandLimit;

			// Main note generation
			while (noteIndex < notes.Length || obsicleIndex < obstacles.Length)
			{
				currentLevel++;
				string commandLevelName = difficultyName + C_LvlName + currentLevel;
				string commandLevelFileName = commandLevelName + C_McFunction;
				string commandLevelFilePath = Path.Combine(_folder_uuidFunctionsPath, commandLevelFileName);
				string currentCommands = "";

				// Continue to generate commands until all nodes and obsicles have been itterated though
				while ((noteIndex < notes.Length || obsicleIndex < obstacles.Length) && currentNumberOfCommands < currentCommandLimit)
				{

					double noteTime = noteIndex < notes.Length ? notes[noteIndex]._time : -1;
					double obsicleTime = obsicleIndex < obstacles.Length ? obstacles[obsicleIndex]._time : -1;

					bool shouldConvertNotes = false;
					bool doBothHaveDataStill = obsicleTime != -1 && noteTime != -1;

					if ((doBothHaveDataStill && noteTime < obsicleTime) || noteTime > 0)
						shouldConvertNotes = true;

					if (shouldConvertNotes)
					{
						if (prevNodeTime != notes[noteIndex]._time)
						{
							prevNodeTime = notes[noteIndex]._time;
							nodeRowID++;
						}

						currentCommands += NodeDataToCommands(notes[noteIndex], _packInfo._beatsPerMinute, _metersPerTick, nodeRowID, ref currentTick);
						prevNodeTime = notes[noteIndex]._time;
						currentNumberOfCommands += 3;
						noteIndex++;
					}
					else
					{
						int numberOfAddedCommands = 0;
						string newCommands = ObsicleDataToCommands(obstacles[obsicleIndex], _packInfo._beatsPerMinute, _metersPerTick, ref numberOfAddedCommands, ref currentTick);
						currentCommands += string.Format("{0}{1}", newCommands, System.Environment.NewLine);
						currentNumberOfCommands += numberOfAddedCommands;
						obsicleIndex++;
					}
				}

				SafeFileManagement.SetFileContents(commandLevelFilePath, currentCommands);
				string baseCommand = string.Format("execute if score @s TickCount matches {0}..{1} run function {2}:{3}",
													prevCurrentTick,
													currentTick,
													_folder_uuid,
													commandLevelName);
				SafeFileManagement.AppendFile(commandBasePath, baseCommand);
				prevCurrentTick = currentTick + 1;
				currentCommandLimit = currentNumberOfCommands + C_CommandLimit;
			}
			difficultyID++;
		}

		SafeFileManagement.AppendFile(difficultiesFunctionPath, _templateStrings._mainMenuBack);
	}

	/// <summary>
	/// Generate commands to produce a beat saber obsicle
	/// </summary>
	/// <param name="obstacle">Obsicle data</param>
	/// <param name="bpm">Beats per minute</param>
	/// <param name="metersPerTick">Distance note travels per tick</param>
	/// <param name="numberOfAddedCommands">Output of number of commands generated</param>
	/// <param name="wholeTick">Output of minecraft tick used</param>
	/// <returns></returns>
	private string ObsicleDataToCommands(_obstacles obstacle, float bpm, double metersPerTick, ref int numberOfAddedCommands, ref int wholeTick)
	{
		string commandList = "";

		double beatsPerTick = bpm / 60.0d / 20;
		double exactTick = obstacle._time / beatsPerTick;

		wholeTick = (int)System.Math.Truncate(exactTick);
		double fractionTick = exactTick % beatsPerTick;
		double fractionMeters = fractionTick * metersPerTick;

		// Calculate the LWH of the rectangular prism to generate
		int lengthOfWallInNotes = (int)System.Math.Truncate(obstacle._duration * 24);

		if (lengthOfWallInNotes == 0)
			lengthOfWallInNotes++;

		int widthOfWallInNotes = obstacle._width;
		int heightOfWallInNotes = obstacle._type == 0 ? 3 : 1;

		for (int length = 0; length < lengthOfWallInNotes; length++)
		{
			double meterLengths = fractionMeters + 0.21 * length;
			int tickOffset = meterLengths != 0 ? (int)System.Math.Truncate(meterLengths / metersPerTick) : 0;
			double extraOffset = meterLengths != 0 ? meterLengths % metersPerTick : 0;

			for (int height = 0; height < heightOfWallInNotes; height++)
			{
				for (int width = 0; width < widthOfWallInNotes; width++)
				{
					string positionCommand = string.Format("execute if score @s TickCount matches {0} at @e[type=armor_stand, tag=nodeSpawnOrgin] run teleport @e[type=armor_stand, tag=nodeCursor] ~{1} ~{2} ~{3:F18}",
							wholeTick + tickOffset,
							0.3 * obstacle._lineIndex + 0.3d * width,
							0.6d - 0.3d * height,
							-extraOffset);

					string spawnWallNode = string.Format("execute if score @s TickCount matches {0} as @e[type=armor_stand, tag=nodeCursor] run function _block_types:box",
							wholeTick + tickOffset);
					commandList += string.Format("{0}{1}{2}{3}",
												positionCommand,
												System.Environment.NewLine,
												spawnWallNode,
												System.Environment.NewLine);
					numberOfAddedCommands += 2;
				}
			}
		}

		return commandList;
	}

	/// <summary>
	/// Generate tellraw commands that displays a difficulty to the user
	/// </summary>
	/// <param name="modeID">NOTSURE</param>
	/// <param name="difficultyName">Name of the difficulty this is for</param>
	/// <param name="folderID"></param>
	/// <returns>Completed commands for tellraw display</returns>
	private string CreateDifficultyDisplay(string modeID, string difficultyName, string folderID)
	{
		string difficultyCommand1 = _templateStrings._difficultyChat;
		difficultyCommand1 = difficultyCommand1.Replace("DIFFNAME", modeID);
		difficultyCommand1 = difficultyCommand1.Replace("VALUE", "1");
		difficultyCommand1 = difficultyCommand1.Replace("DIFFICULTY", difficultyName);
		difficultyCommand1 = difficultyCommand1.Replace("COLOR", "green");
		difficultyCommand1 = difficultyCommand1.Replace("folder_uuid", folderID);

		string difficultyCommand2 = _templateStrings._difficultyChat;
		difficultyCommand2 = difficultyCommand2.Replace("DIFFNAME", modeID);
		difficultyCommand2 = difficultyCommand2.Replace("VALUE", "0");
		difficultyCommand2 = difficultyCommand2.Replace("DIFFICULTY", difficultyName);
		difficultyCommand2 = difficultyCommand2.Replace("COLOR", "red");
		difficultyCommand2 = difficultyCommand2.Replace("folder_uuid", folderID);

		return difficultyCommand1 + System.Environment.NewLine + difficultyCommand2;
	}

	/// <summary>
	/// Generate all neciccary commands for node placement on beat
	/// </summary>
	/// <param name="node">Node data to use</param>
	/// <param name="bpm">Beats per minute</param>
	/// <param name="metersPerTick">Distance note travels per tick</param>
	/// <param name="nodeRowID">Row id within minecraft</param>
	/// <param name="wholeTick">Output of minecraft tick used</param>
	/// <returns></returns>
	private string NodeDataToCommands(_notes node, float bpm, double metersPerTick, int nodeRowID, ref int wholeTick)
	{
		//_lineIndex = col
		//_lineLayer = row

		double beatsPerTick = bpm / 60.0d / 20;
		double exactTick = node._time / beatsPerTick;

		wholeTick = (int)Mathf.Floor((float)exactTick);
		double fractionTick = exactTick % beatsPerTick;
		double fractionMeters = fractionTick * metersPerTick;

		string scoreCommand = string.Format("execute if score @s TickCount matches {0} at @e[type=armor_stand,tag=playerOrgin] as @p[scores={{SongID={1}}}] run scoreboard players set @s NodeRowID {2}",
			wholeTick,
			_keyVars["SONGID"],
			nodeRowID);

		return string.Format("{0}{1}{2}{3}{4}{5}",
							NodePositionCommand(wholeTick, node._lineIndex * 0.3d, node._lineLayer * 0.3d, -fractionMeters),
							System.Environment.NewLine,
							scoreCommand,
							System.Environment.NewLine,
							NodeTypeCommand(wholeTick, node),
							System.Environment.NewLine);
	}

	/// <summary>
	/// Convert given tick and xyz offset into a position command in minecraft
	/// </summary>
	/// <param name="tick">whole tick when command will be ran</param>
	/// <param name="xOffset">Minecraft x offset</param>
	/// <param name="yOffset">Minecraft y offset</param>
	/// <param name="zOffset">Minecraft z offset</param>
	/// <returns></returns>
	private string NodePositionCommand(int tick, double xOffset, double yOffset, double zOffset)
	{
		return string.Format("execute if score @s TickCount matches {0} at @e[type=armor_stand, tag=nodeSpawnOrgin] run teleport @e[type=armor_stand, tag=nodeCursor] ~{1} ~{2} ~{3:F18}",
							tick,
							xOffset,
							yOffset,
							zOffset);
	}

	/// <summary>
	/// Convert a tick and node into a node generation command
	/// </summary>
	/// <param name="tick">whole tick when command will be ran</param>
	/// <param name="node">node data to generate</param>
	/// <returns></returns>
	private string NodeTypeCommand(int tick, _notes node)
	{
		return string.Format("execute if score @s TickCount matches {0} as @e[type=armor_stand, tag=nodeCursor] run function _block_types:{1}",
							tick,
							_noteTypes[node._type][node._cutDirection]);
	}
}
