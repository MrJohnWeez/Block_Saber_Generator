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

	// File names
	private const string C_EasyFunction = "easy.mcfunction";
	private const string C_NormalFunction = "normal.mcfunction";
	private const string C_HardFunction = "hard.mcfunction";
	private const string C_ExpertFunction = "expert.mcfunction";
	private const string C_ExpertPlusFunction = "expert_plus.mcfunction";
	private const string C_PlayFunction = "play.mcfunction";
	private const string C_SpawnNotesBaseFunction = "spawn_notes_base.mcfunction";

	// Part names
	private const string C_LvlName = "_lvl_";

	// Paths
	private string _datapackRootPath = "";
	private string _blockSaberBaseFunctionsPath = "";
	private string _blockSaberSongFunctionsPath = "";
	private string _spawnNotesBasePath = "";

	// Counters
	private int _difficulty = 0;


	private string _pathOfDatapackTemplate = Path.Combine(C_StreamingAssets, "TemplateDatapack");

	private List<BeatMapSong> _beatMapSongList;

	private double _metersPerTick = 0;


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
		// Names
		_longNameID = _packInfo._songAuthorName + " - " + _packInfo._songName + " " + _packInfo._songSubName;
		_packName = C_BlockSaber + _packInfo._songAuthorName + " - " + _packInfo._songName + " " + _packInfo._songSubName;

		// Paths
		_datapackRootPath = Path.Combine(_unzippedFolderPath, _packName);
		_fullOutputPath = Path.Combine(_outputPath, _packName + C_Zip);
		_blockSaberBaseFunctionsPath = Path.Combine(_datapackRootPath, C_Data, C_BlockSaberBase, C_Functions);
		_blockSaberSongFunctionsPath = Path.Combine(_datapackRootPath, C_Data, C_BlockSaberSong, C_Functions);
		_spawnNotesBasePath = Path.Combine(_blockSaberBaseFunctionsPath, C_SpawnNotesBaseFunction);

		_metersPerTick = CalculateMoveSpeed(_packInfo._beatsPerMinute);

		// Keys
		_keyVars["MAPPER_NAME"] = _packInfo._levelAuthorName;
		_keyVars["BEATS_PER_MINUTE"] = _packInfo._beatsPerMinute.ToString();
		_keyVars["SONGUUID"] = Random.Range(-999999999, 999999999).ToString();
		_keyVars["MOVESPEED"] = _metersPerTick.ToString();
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


			// Main note generation
			while (noteIndex < notes.Length)
			{
				currentLevel++;
				string commandLevelName = string.Format("{0}{1}{2}", difficultyName, C_LvlName, currentLevel);
				string commandLevelFileName = string.Format("{0}{1}", commandLevelName, C_McFunction);
				string commandLevelFilePath = Path.Combine(_blockSaberSongFunctionsPath, commandLevelFileName);
				string currentCommands = "";

				while (noteIndex < notes.Length && currentNumberOfCommands < C_CommandLimit)
				{
					currentCommands += NodeDataToCommands(notes[noteIndex], _metersPerTick, _packInfo._beatsPerMinute, ref currentTick);
					currentNumberOfCommands += 2;
					noteIndex++;
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
		}
	}

	private double CalculateMoveSpeed(float beatsPerMinute)
	{
		return beatsPerMinute / 60.0d * 24 * 0.21 / 20;
	}

	private string NodeDataToCommands(_notes node, double moveSpeed, float bpm, ref int wholeTick)
	{
		//_lineIndex = col
		//_lineLayer = row

		double beatsPerTick = bpm / 60.0d / 20;
		double exactTick = node._time / beatsPerTick;

		wholeTick = (int)Mathf.Floor((float)exactTick);
		double fractionTick = exactTick % beatsPerTick;
		double fractionMeters = fractionTick * _metersPerTick;

		return string.Format("{0}{1}{2}{3}",
							NodePositionCommand(wholeTick, node._lineIndex * 0.3d, node._lineLayer * 0.3d, -fractionMeters),
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
