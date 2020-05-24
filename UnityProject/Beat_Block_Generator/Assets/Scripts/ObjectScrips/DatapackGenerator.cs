using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DatapackGenerator : GeneratorBase
{
	private const int C_CommandLimit = 10000;
	private readonly string[][] noteTypes = new string[4][] {new string[9]
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
																{
																	"","","","","","","","","bomb"
																}
															};

	// Extension names
	private const string C_McFunction = ".mcfunction";

	// Symbols
	private const string C_FakePlayerChar = "#";
	private const string C_Slash = "/";

	// Folder Names
	private const string C_Data = "data";
	private const string C_Functions = "functions";
	private const string C_BlockSaber = "Block_Saber_";
	private const string C_Tags = "tags";
	private const string C_TemplateName = "BlockSaberTemplate";
	private const string C_BlockSaberBase = "block_saber_base";
	private const string C_BlockSaberSong = "block_saber_song";
	private const string C_Chat = "chat";

	// File names
	private const string C_EasyFunction = "easy.mcfunction";
	private const string C_NormalFunction = "normal.mcfunction";
	private const string C_HardFunction = "hard.mcfunction";
	private const string C_ExpertFunction = "expert.mcfunction";
	private const string C_ExpertPlusFunction = "expert_plus.mcfunction";
	private const string C_PlayFunction = "play.mcfunction";
	private const string C_SpawnNotesBaseFunction = "spawn_notes_base.mcfunction";
	private const string C_InitFunction = "init.mcfunction";
	private const string C_Difficulties = "difficulties.mcfunction";
	private const string C_TemplateStrings = "TemplateStrings.json";


	// Part names
	private const string C_LvlName = "_lvl_";

	// Paths
	private string _datapackRootPath = "";
	private string _blockSaberBaseFunctionsPath = "";
	private string _blockSaberSongFunctionsPath = "";
	private string _blockSaberChatFunctionsPath = "";
	private string _spawnNotesBasePath = "";

	// Counters
	private int _difficulty = 0;


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

	protected override void Init()
	{
		base.Init();
		// minecraft data
		string _pathOfTemplateStrings = Path.Combine(C_StreamingAssets, C_TemplateStrings);
		_templateStrings = JsonUtility.FromJson<TemplateStrings>(SafeFileManagement.GetFileContents(_pathOfTemplateStrings));


		// Names
		_longNameID = _packInfo._songAuthorName + " - " + _packInfo._songName + " " + _packInfo._songSubName;
		_packName = C_BlockSaber + _packInfo._songAuthorName + " - " + _packInfo._songName + " " + _packInfo._songSubName;

		// Paths
		_datapackRootPath = Path.Combine(_unzippedFolderPath, _packName);
		_fullOutputPath = Path.Combine(_outputPath, _packName + C_Zip);
		_blockSaberBaseFunctionsPath = Path.Combine(_datapackRootPath, C_Data, C_BlockSaberBase, C_Functions);
		_blockSaberSongFunctionsPath = Path.Combine(_datapackRootPath, C_Data, C_BlockSaberSong, C_Functions);
		_blockSaberChatFunctionsPath = Path.Combine(_datapackRootPath, C_Data, C_Chat, C_Functions);
		_spawnNotesBasePath = Path.Combine(_blockSaberBaseFunctionsPath, C_SpawnNotesBaseFunction);

		_metersPerTick = CalculateMoveSpeed(_packInfo._beatsPerMinute);

		_UUIDHex = Random.Range(-999999999, 999999999).ToString("X");
		_offset = _metersPerTick * -21;

		// Keys
		_keyVars["MAPPER_NAME"] = _packInfo._levelAuthorName;
		_keyVars["BEATS_PER_MINUTE"] = _packInfo._beatsPerMinute.ToString();
		_keyVars["SONGUUID"] = Random.Range(-999999999, 999999999).ToString();
		_keyVars["MOVESPEED"] = _metersPerTick.ToString();
		_keyVars["SONGTITLE"] = _packInfo._songName + " " + _packInfo._songSubName;
		_keyVars["SONGARTIST"] = _packInfo._songAuthorName;

		_keyVars["OFFSET"] = string.Format("{0:F18}", _offset);
		_offset = double.Parse(_keyVars["OFFSET"]);

		_keyVars["SONG"] = (_packInfo._songAuthorName + " - " + _packInfo._songName + " " + _packInfo._songSubName).MakeMinecraftSafe();
	}

	/// <summary>
	/// Update all files within a directory to correct varible names
	/// </summary>
	/// <param name="folderPath">In folder path</param>
	private void UpdateAllCopiedFiles(string folderPath, bool checkSubDirectories = false)
	{
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

	private void GenerateMCBeatData()
	{
		string difficultiesFunctionPath = Path.Combine(_blockSaberChatFunctionsPath, C_Difficulties);
		string initFunctionPath = Path.Combine(_blockSaberBaseFunctionsPath, C_InitFunction);
		int difficultyID = 1;
		double prevNodeTime = 0;
		int nodeRowID = 1;


		foreach (BeatMapSong song in _beatMapSongList)
		{
			string difficultyName = song.difficultyBeatmaps._difficulty.MakeMinecraftSafe();
			Debug.Log("Generating song for mode: " + difficultyName);

			_difficulty++;
			string spawnNotesBaseCommand = string.Format("execute if score @s Difficulty matches {0} as @s run function block_saber_song:{1}",
														_difficulty,
														difficultyName);
			SafeFileManagement.AppendFile(_spawnNotesBasePath, spawnNotesBaseCommand);

			string commandBasePath = Path.Combine(_blockSaberSongFunctionsPath, difficultyName + C_McFunction);

			int currentLevel = 0;
			int currentTick = 0;
			int prevCurrentTick = 0;
			int currentNumberOfCommands = 0;

			_notes[] notes = song.beatMapData._notes;
			_obstacles[] obstacles = song.beatMapData._obstacles;

			int noteIndex = 0;
			int obsicleIndex = 0;
			string modeID = _UUIDHex + difficultyID.ToString();

			string playCommands = string.Format("scoreboard players set @s Difficulty {0}{1}execute as @s run function block_saber_base:play",
												difficultyID,
												System.Environment.NewLine);


			string scoreboardCommand = string.Format("scoreboard objectives add {0} dummy{1}execute as @a unless score @s {0} = @s {0} run scoreboard players set @s {0} 0",
													modeID,
													System.Environment.NewLine);

			SafeFileManagement.AppendFile(initFunctionPath, scoreboardCommand);


			string difficultyCommand1 = _templateStrings._difficultyChat;
			difficultyCommand1 = difficultyCommand1.Replace("DIFFNAME", modeID);
			difficultyCommand1 = difficultyCommand1.Replace("VALUE", "1");
			difficultyCommand1 = difficultyCommand1.Replace("DIFFICULTY", difficultyName);
			difficultyCommand1 = difficultyCommand1.Replace("COLOR", "green");

			string difficultyCommand2 = _templateStrings._difficultyChat;
			difficultyCommand2 = difficultyCommand2.Replace("DIFFNAME", modeID);
			difficultyCommand2 = difficultyCommand2.Replace("VALUE", "0");
			difficultyCommand2 = difficultyCommand2.Replace("DIFFICULTY", difficultyName);
			difficultyCommand2 = difficultyCommand2.Replace("COLOR", "red");

			SafeFileManagement.AppendFile(difficultiesFunctionPath, difficultyCommand1 + System.Environment.NewLine + difficultyCommand2);




			string playPath = Path.Combine(_blockSaberSongFunctionsPath, difficultyName + "_play" + C_McFunction);
			SafeFileManagement.SetFileContents(playPath, playCommands);

			// Main note generation
			while (noteIndex < notes.Length || obsicleIndex < obstacles.Length)
			{
				currentLevel++;
				string commandLevelName = string.Format("{0}{1}{2}", difficultyName, C_LvlName, currentLevel);
				string commandLevelFileName = string.Format("{0}{1}", commandLevelName, C_McFunction);
				string commandLevelFilePath = Path.Combine(_blockSaberSongFunctionsPath, commandLevelFileName);
				string currentCommands = "";

				while((noteIndex < notes.Length || obsicleIndex < obstacles.Length) && currentNumberOfCommands < C_CommandLimit)
				{
					double noteTime = noteIndex < notes.Length ? notes[noteIndex]._time : -1;
					double obsicleTime = obsicleIndex < obstacles.Length ? obstacles[obsicleIndex]._time : -1;

					if(obsicleTime != -1 && noteTime != -1)
					{
						// Both notes still
						if (noteTime < obsicleTime)
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
						else if (obsicleTime < noteTime)
						{
							int numberOfAddedCommands = 0;
							string newCommands = ObsicleDataToCommands(obstacles[obsicleIndex], _packInfo._beatsPerMinute, _metersPerTick, ref numberOfAddedCommands, ref currentTick);
							currentCommands += string.Format("{0}{1}", newCommands, System.Environment.NewLine);
							currentNumberOfCommands += numberOfAddedCommands;
							obsicleIndex++;
						}
					}
					else if(noteTime > 0)
					{
						// Just notes left
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
					else if (obsicleTime > 0)
					{
						// Just obsicles left
						int numberOfAddedCommands = 0;
						currentCommands += ObsicleDataToCommands(obstacles[obsicleIndex], _packInfo._beatsPerMinute, _metersPerTick, ref numberOfAddedCommands, ref currentTick);
						currentNumberOfCommands += numberOfAddedCommands;
						obsicleIndex++;
					}
				}

				SafeFileManagement.SetFileContents(commandLevelFilePath, currentCommands);
				string baseCommand = string.Format("execute if score @s TickCount matches {0}..{1} run function {2}:{3}",
													prevCurrentTick,
													currentTick,
													C_BlockSaberSong,
													commandLevelName);
				SafeFileManagement.AppendFile(commandBasePath, baseCommand);
				prevCurrentTick = currentTick + 1;
			}
			difficultyID++;
		}

		SafeFileManagement.AppendFile(difficultiesFunctionPath, _templateStrings._mainMenuBack);
	}

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

		for(int length = 0; length < lengthOfWallInNotes; length++)
		{
			double meterLengths = fractionMeters + 0.21 * length;
			int tickOffset = meterLengths != 0 ? (int)System.Math.Truncate(meterLengths / metersPerTick) : 0;
			double extraOffset = meterLengths != 0 ? meterLengths % metersPerTick : 0;
			for (int height = 0; height < heightOfWallInNotes; height++)
			{
				for (int width = 0; width < widthOfWallInNotes; width++)
				{
					string positionCommand = string.Format("execute if score @s TickCount matches {0} at @e[type = armor_stand, tag = nodeSpawnOrgin] run teleport @e[type = armor_stand, tag = nodeCursor] ~{1} ~{2} ~{3:F18}",
							wholeTick + tickOffset,
							0.3 * obstacle._lineIndex + 0.3d * width,
							0.6d - 0.3d * height,
							-extraOffset);

					string spawnWallNode = string.Format("execute if score @s TickCount matches {0} as @e[type = armor_stand, tag = nodeCursor] run function block_types:box",
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
	
	private double CalculateMoveSpeed(float beatsPerMinute)
	{
		return beatsPerMinute / 60.0d * 24 * 0.21 / 20;
	}

	private string NodeDataToCommands(_notes node, float bpm, double metersPerTick, int nodeRowID, ref int wholeTick)
	{
		//_lineIndex = col
		//_lineLayer = row

		double beatsPerTick = bpm / 60.0d / 20;
		double exactTick = node._time / beatsPerTick;

		wholeTick = (int)Mathf.Floor((float)exactTick);
		double fractionTick = exactTick % beatsPerTick;
		double fractionMeters = fractionTick * metersPerTick;

		string scoreCommand = string.Format("execute if score @s TickCount matches {0} at @e[type=armor_stand,tag=playerOrgin] as @p[scores={{SongUUID={1}}}] run scoreboard players set @s NodeRowID {2}",
											wholeTick,
											_keyVars["SONGUUID"],
											nodeRowID);
		
		return string.Format("{0}{1}{2}{3}{4}{5}",
							NodePositionCommand(wholeTick, node._lineIndex * 0.3d, node._lineLayer * 0.3d, -fractionMeters),
							System.Environment.NewLine,
							scoreCommand,
							System.Environment.NewLine,
							NodeTypeCommand(wholeTick, node),
							System.Environment.NewLine);
	}

	private string NodePositionCommand(int tick, double xOffset, double yOffset, double zOffset)
	{
		return string.Format("execute if score @s TickCount matches {0} at @e[type = armor_stand, tag = nodeSpawnOrgin] run teleport @e[type = armor_stand, tag = nodeCursor] ~{1} ~{2} ~{3:F18}",
							tick,
							xOffset,
							yOffset,
							zOffset);
	}

	private string NodeTypeCommand(int tick, _notes node)
	{

		return string.Format("execute if score @s TickCount matches {0} as @e[type = armor_stand, tag = nodeCursor] run function block_types:{1}",
							tick,
							noteTypes[node._type][node._cutDirection]);
	}
}
