using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using UnityEngine;

/// <summary>
/// Generate entire datapack based on given beat saber map data. 
/// NOTE: Ref is used alot for performance reasons
/// </summary>
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
																	{"bomb","bomb","bomb","bomb","bomb","bomb","bomb","bomb","bomb"}};

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
	private const string C_SetSpawnOrgin = "set_spawn_orgin.mcfunction";
	private const string C_TemplateStrings = "TemplateStrings.json";

	// Part names
	private const string C_LvlNoteName = "_lvl_note_";
	private const string C_LvlObsicleName = "_lvl_obsicle_";

	// Holds all minecraft command templates
	private static TemplateStrings _templateStrings = null;

	// Paths
	private string _datapackRootPath = "";
	private string _blockSaberBaseFunctionsPath = "";
	private string _spawnNotesBasePath = "";
	private string _folder_uuidFunctionsPath = "";
	private string _pathOfDatapackTemplate = Path.Combine(C_StreamingAssets, "TemplateDatapack");

	private List<BeatMapSong> _beatMapSongList;
	private double _metersPerTick = 0;
	private string _UUIDVar = "";

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
	/// <returns>True if successful</returns>
	public override bool Generate()
	{
		if (Directory.Exists(_unzippedFolderPath) && _packInfo != null && _beatMapSongList.Count > 0)
		{
			Debug.Log("Copying Template...");
			if (CopyTemplate(_pathOfDatapackTemplate, _unzippedFolderPath))
			{
				Debug.Log("Updating Copied files...");
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
		if(_templateStrings == null)
		{
			string _pathOfTemplateStrings = Path.Combine(C_StreamingAssets, C_TemplateStrings);
			_templateStrings = JsonUtility.FromJson<TemplateStrings>(SafeFileManagement.GetFileContents(_pathOfTemplateStrings));
		}
		
		// Names
		_folder_uuid = SafeFileManagement.GetFileName(Path.GetFileName(_unzippedFolderPath)).MakeMinecraftSafe();
		_packName = C_Datapack + _folder_uuid;
		_UUIDVar = UnityEngine.Random.Range(-999999999, 999999999).ToString("X");

		// Paths
		_datapackRootPath = Path.Combine(_unzippedFolderPath, _packName);
		_fullOutputPath = Path.Combine(_outputPath, _packName + C_Zip);
		_blockSaberBaseFunctionsPath = Path.Combine(_datapackRootPath, C_Data, C_BlockSaberBase, C_Functions);
		_folder_uuidFunctionsPath = Path.Combine(_datapackRootPath, C_Data, _folder_uuid, C_Functions);
		_spawnNotesBasePath = Path.Combine(_folder_uuidFunctionsPath, C_SpawnNotesBaseFunction);

		// Values
		_metersPerTick = _packInfo._beatsPerMinute  / 60.0d * 24 * 0.21 / 20;

		// Set up Keys
		_keyVars["MAPPER_NAME"] = _packInfo._levelAuthorName;
		_keyVars["BEATS_PER_MINUTE"] = _packInfo._beatsPerMinute.ToString();
		System.Random rand = new System.Random();
		_keyVars["SONGID"] = LongRandom(-99999999999, 99999999999, rand).ToString("X");
		_keyVars["MOVESPEED"] = _metersPerTick.ToString();
		_keyVars["SONGTITLE"] = _packInfo._songName + " " + _packInfo._songSubName;
		_keyVars["SONGSHORTNAME"] = _packInfo._songName;
		_keyVars["SONGARTIST"] = _packInfo._songAuthorName;
		_keyVars["folder_uuid"] = _folder_uuid;

		string listOfDifficulties = "";
		for(int diffNumber = 0; diffNumber < _beatMapSongList.Count; diffNumber++)
		{
			listOfDifficulties += _beatMapSongList[diffNumber].difficultyBeatmaps._difficulty;
			if (diffNumber < _beatMapSongList.Count - 1)
				listOfDifficulties += " | ";
		}
		_keyVars["DIFFICULTYLIST"] = listOfDifficulties;
	}

	/// <summary>
	/// Update all files within a directory to correct varible names
	/// </summary>
	/// <param name="folderPath">Root folder path to scan</param>
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
		StringBuilder difficultyDisplayCommands = new StringBuilder();
		StringBuilder scoreboardCommands = new StringBuilder();
		StringBuilder spawnOrginCommands = new StringBuilder();
		StringBuilder spawnNotesBaseCommands = new StringBuilder();
		int difficultyNumber = 1;
		
		// Itterate though each song difficulty
		foreach (BeatMapSong song in _beatMapSongList)
		{
			double ticksStartOffset = (int)(Mathf.Clamp((float)(_packInfo._beatsPerMinute / 60d * 10), 7, 20) / _metersPerTick);
			string difficultyName = song.difficultyBeatmaps._difficulty.MakeMinecraftSafe();
			
			// Append running command lists
			string songDifficultyID = _UUIDVar + difficultyNumber.ToString();
			scoreboardCommands.AppendFormat(_templateStrings._scoreboardCommand,
											songDifficultyID);

			spawnOrginCommands.AppendFormat(_templateStrings._spawnOrginCommands,
											-_metersPerTick * ticksStartOffset,
											difficultyNumber);

			spawnNotesBaseCommands.AppendFormat(_templateStrings._spawnNotesBaseCommand,
												difficultyNumber,
												_folder_uuid,
												difficultyName);

			CreateDifficultyDisplay(songDifficultyID, difficultyName, _folder_uuid, ref difficultyDisplayCommands);


			// Write difficulty-specific-file commands
			string playCommands = string.Format(_templateStrings._playCommands,
												difficultyNumber,
												_folder_uuid);
			string playPath = Path.Combine(_folder_uuidFunctionsPath, difficultyName + "_play" + C_McFunction);
			SafeFileManagement.SetFileContents(playPath, playCommands);


			string playSongCommand = string.Format(_templateStrings._playSongCommand,
													ticksStartOffset - 1,
													_folder_uuid);
			string commandBasePath = Path.Combine(_folder_uuidFunctionsPath, difficultyName + C_McFunction);
			SafeFileManagement.AppendFile(commandBasePath, playSongCommand);
			
			// Generate main note/obsicle data
			GenerateNotes(song, difficultyName, commandBasePath);
			GenerateObsicles(song, difficultyName, commandBasePath);
			
			difficultyNumber++;
		}

		// Write collected commands to files
		string difficultiesFunctionPath = Path.Combine(_folder_uuidFunctionsPath, C_Difficulties);
		string initFunctionPath = Path.Combine(_folder_uuidFunctionsPath, C_InitFunction);
		string setSpawnOrginFunctionPath = Path.Combine(_folder_uuidFunctionsPath, C_SetSpawnOrgin);

		SafeFileManagement.AppendFile(_spawnNotesBasePath, spawnNotesBaseCommands.ToString());
		SafeFileManagement.AppendFile(setSpawnOrginFunctionPath, spawnOrginCommands.ToString());
		SafeFileManagement.AppendFile(initFunctionPath, scoreboardCommands.ToString());

		// Add back button in tellraw
		difficultyDisplayCommands.Append(_templateStrings._mainMenuBack);
		SafeFileManagement.AppendFile(difficultiesFunctionPath, difficultyDisplayCommands.ToString());
	}

	/// <summary>
	/// Generate note commands given a song and difficulty
	/// </summary>
	/// <param name="song">Beatmap data for a song and difficulty</param>
	/// <param name="difficultyName">Minecraft safe difficulty name</param>
	/// <param name="commandBasePath">Base folder path to generate new mcfunctions</param>
	private void GenerateNotes(BeatMapSong song, string difficultyName, string commandBasePath)
	{
		_notes[] notes = song.beatMapData._notes;

		double prevNodeTime = 0;
		int nodeRowID = 1;
		int currentLevel = 0;
		int currentTick = 0;
		int prevCurrentTick = 0;
		int currentNumberOfCommands = 0;
		int noteIndex = 0;
		int currentCommandLimit = C_CommandLimit;

		// Main note generation
		while (noteIndex < notes.Length)
		{
			currentLevel++;
			string commandLevelName = difficultyName + C_LvlNoteName + currentLevel;
			string commandLevelFileName = commandLevelName + C_McFunction;
			string commandLevelFilePath = Path.Combine(_folder_uuidFunctionsPath, commandLevelFileName);
			StringBuilder currentCommands = new StringBuilder();

			// Continue to generate commands until all nodes and obsicles have been itterated though
			while (noteIndex < notes.Length && currentNumberOfCommands < currentCommandLimit)
			{
				if (prevNodeTime != notes[noteIndex]._time)
				{
					prevNodeTime = notes[noteIndex]._time;
					nodeRowID++;
				}

				NodeDataToCommands(notes[noteIndex], _packInfo._beatsPerMinute, _metersPerTick, nodeRowID, ref currentCommands, ref currentTick);
				prevNodeTime = notes[noteIndex]._time;
				currentNumberOfCommands += 3;
				noteIndex++;
			}

			SafeFileManagement.SetFileContents(commandLevelFilePath, currentCommands.ToString());
			string baseCommand = string.Format(_templateStrings._baseCommand,
												prevCurrentTick,
												currentTick,
												_folder_uuid,
												commandLevelName);
			SafeFileManagement.AppendFile(commandBasePath, baseCommand);
			prevCurrentTick = currentTick + 1;
			currentCommandLimit = currentNumberOfCommands + C_CommandLimit;
		}
	}

	/// <summary>
	/// Generate obsicles commands given a song and difficulty
	/// </summary>
	/// <param name="song">Beatmap data for a song and difficulty</param>
	/// <param name="difficultyName">Minecraft safe difficulty name</param>
	/// <param name="commandBasePath">Base folder path to generate new mcfunctions</param>
	private void GenerateObsicles(BeatMapSong song, string difficultyName, string commandBasePath)
	{
		_obstacles[] obstacles = song.beatMapData._obstacles;
		int obsicleIndex = 0;
		int currentLevel = 0;
		int currentTick = 0;
		int maxTick = 0;
		int minTick = -1;
		int prevCurrentTick = 0;
		int currentNumberOfCommands = 0;
		int currentCommandLimit = C_CommandLimit;

		// Main note generation
		while (obsicleIndex < obstacles.Length)
		{
			currentLevel++;
			string commandLevelName = difficultyName + C_LvlObsicleName + currentLevel;
			string commandLevelFileName = commandLevelName + C_McFunction;
			string commandLevelFilePath = Path.Combine(_folder_uuidFunctionsPath, commandLevelFileName);
			StringBuilder currentCommands = new StringBuilder();
			int maxNewTick = 0;
			int minNewTick = 0;

			// Continue to generate commands until all nodes and obsicles have been itterated though
			while (obsicleIndex < obstacles.Length && currentNumberOfCommands < currentCommandLimit)
			{
				ObsicleDataToCommands(obstacles[obsicleIndex], _packInfo._beatsPerMinute, _metersPerTick, ref currentCommands, ref currentNumberOfCommands, ref minNewTick, ref maxNewTick);
				if (minTick == -1)
					minTick = minNewTick;

				maxTick = Mathf.Max(maxTick, maxNewTick);
				obsicleIndex++;
			}
			SafeFileManagement.SetFileContents(commandLevelFilePath, currentCommands.ToString());
			string baseCommand = string.Format(_templateStrings._baseCommand,
												minTick,
												maxTick,
												_folder_uuid,
												commandLevelName);
			SafeFileManagement.AppendFile(commandBasePath, baseCommand);
			prevCurrentTick = currentTick + 1;
			currentCommandLimit = currentNumberOfCommands + C_CommandLimit;
			minTick = 0;
			maxTick = 0;
		}
	}


	/// <summary>
	/// Generate commands to produce a beat saber obsicle
	/// </summary>
	/// <param name="obstacle">Obsicle data</param>
	/// <param name="bpm">Beats per minute</param>
	/// <param name="metersPerTick">Distance note travels per tick</param>
	/// <param name="commandList">String Builder object to append to</param>
	/// <param name="addToNumberOfCommands">Output of number of commands generated</param>
	/// <param name="minWholeTick">Output of min minecraft tick used</param>
	/// <param name="maxWholeTick">Output of max minecraft tick used</param>
	private void ObsicleDataToCommands(_obstacles obstacle, float bpm, double metersPerTick, ref StringBuilder commandList, ref int addToNumberOfCommands, ref int minWholeTick, ref int maxWholeTick)
	{
		double beatsPerTick = bpm / 60.0d / 20;
		double exactTick = obstacle._time / beatsPerTick;

		minWholeTick = (int)System.Math.Truncate(exactTick);
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
					maxWholeTick = minWholeTick + tickOffset;
					commandList.AppendFormat(_templateStrings._positionCommand,
											maxWholeTick,
											0.3 * obstacle._lineIndex + 0.3d * width,
											0.6d - 0.3d * height,
											-extraOffset);
					addToNumberOfCommands += 2;
				}
			}
		}
	}
	

	/// <summary>
	/// Generate all neciccary commands for node placement on beat
	/// </summary>
	/// <param name="node">Node data to use</param>
	/// <param name="bpm">Beats per minute</param>
	/// <param name="metersPerTick">Distance note travels per tick</param>
	/// <param name="nodeRowID">Row id within minecraft</param>
	/// <param name="commandList">String Builder object to append to</param>
	/// <param name="wholeTick">Output of minecraft tick used</param>
	private void NodeDataToCommands(_notes node, float bpm, double metersPerTick, int nodeRowID, ref StringBuilder commandList, ref int wholeTick)
	{
		//_lineIndex = col
		//_lineLayer = row

		double beatsPerTick = bpm / 60.0d / 20;
		double exactTick = node._time / beatsPerTick;

		wholeTick = (int)Mathf.Floor((float)exactTick);
		double fractionTick = exactTick % beatsPerTick;
		double fractionMeters = fractionTick * metersPerTick;

		commandList.AppendFormat(_templateStrings._nodePositionCommand,
									wholeTick,
									node._lineIndex * 0.3d,
									node._lineLayer * 0.3d,
									-fractionMeters);
		commandList.AppendFormat(_templateStrings._scoreCommand,
									wholeTick,
									_keyVars["SONGID"],
									nodeRowID);
		commandList.AppendFormat(_templateStrings._nodeTypeCommand,
									wholeTick,
									_noteTypes[node._type][node._cutDirection]);
	}

	/// <summary>
	/// Generate tellraw commands that displays a difficulty to the user
	/// </summary>
	/// <param name="modeID">NOTSURE</param>
	/// <param name="difficultyName">Name of the difficulty this is for</param>
	/// <param name="folderID"></param>
	/// <param name="commandList">String Builder object to append to</param>
	private void CreateDifficultyDisplay(string modeID, string difficultyName, string folderID, ref StringBuilder commandList)
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

		commandList.Append(difficultyCommand1);
		commandList.Append(difficultyCommand2);
	}

	/// <summary>
	/// Generate random Long number given range
	/// </summary>
	/// <param name="min">Min number</param>
	/// <param name="max">Max number</param>
	/// <param name="rand">Random object</param>
	/// <returns></returns>
	private long LongRandom(long min, long max, System.Random rand)
	{
		
		byte[] buf = new byte[8];
		rand.NextBytes(buf);
		long longRand = BitConverter.ToInt64(buf, 0);

		return (Math.Abs(longRand % (max - min)) + min);
	}
}
