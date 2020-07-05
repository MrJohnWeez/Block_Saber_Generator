// Created by MrJohnWeez
// June 2020

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;
using BeatSaber.packInfo;
using BeatSaber;
using BeatSaber.beatMapData.notes;
using MrJohnWeez.Extensions;

namespace Minecraft.Generator
{
	public static class DataPack
	{
		/// <summary>
		/// Generate a minecraft datapack from Beat Saber data
		/// </summary>
		public static int FromBeatSaberData(string unzippedFolderPath, string datapackOutputPath, PackInfo packInfo, List<BeatMapSong> beatMapSongList)
		{
			// Validate inputs
			if (!Directory.Exists(unzippedFolderPath) || packInfo == null || beatMapSongList == null)
				return 0;

			DataPackData dpd = new DataPackData(unzippedFolderPath, datapackOutputPath, packInfo, beatMapSongList);

			if (beatMapSongList.Count > 0)
			{
				Debug.Log("Copying Template...");
				string copiedTemplatePath = Path.Combine(unzippedFolderPath, Globals.C_TemplateDataPackName);
				if (SafeFileManagement.DirectoryCopy(Globals.pathOfDatapackTemplate, unzippedFolderPath, true, Globals.excludeExtensions, Globals.C_numberOfIORetryAttempts))
				{
					if (SafeFileManagement.MoveDirectory(copiedTemplatePath, dpd.datapackRootPath, Globals.C_numberOfIORetryAttempts))
					{
						// Must change the folder names before searching for keys
						string songname_uuidFolder = Path.Combine(dpd.datapackRootPath, Globals.C_Data, Globals.C_FolderUUID);
						string newPath = Path.Combine(dpd.datapackRootPath, Globals.C_Data, dpd.folder_uuid);
						SafeFileManagement.MoveDirectory(songname_uuidFolder, newPath, Globals.C_numberOfIORetryAttempts);

						Debug.Log("Updating Copied files...");
						Filemanagement.UpdateAllCopiedFiles(dpd.datapackRootPath, dpd.keyVars, true, Globals.excludeKeyVarExtensions);

						Debug.Log("Generating main datapack files...");
						GenerateMCBeatData(beatMapSongList, packInfo, dpd);

						Debug.Log("Zipping files...");
						Archive.Compress(dpd.datapackRootPath, dpd.fullOutputPath);

						Debug.Log("Datapack Done");
						return -1;
					}
				}
			}
			return 0;
		}

		/// <summary>
		/// Main generation of minecraft commands for beat saber data
		/// </summary>
		public static void GenerateMCBeatData(List<BeatMapSong> beatMapSongList, PackInfo packInfo, DataPackData dpd)
		{
			StringBuilder difficultyDisplayCommands = new StringBuilder();
			StringBuilder scoreboardCommands = new StringBuilder();
			StringBuilder spawnOrginCommands = new StringBuilder();
			StringBuilder spawnNotesBaseCommands = new StringBuilder();
			int difficultyNumber = 1;

			// Itterate though each song difficulty
			foreach (BeatMapSong song in beatMapSongList)
			{
				string difficultyName = song.difficultyBeatmaps._difficulty.MakeMinecraftSafe();

				// Append running command lists
				string songDifficultyID = dpd.UUIDVar + difficultyNumber.ToString();
				scoreboardCommands.AppendFormat(Globals.templateStrings._scoreboardCommand,
												songDifficultyID);

				spawnOrginCommands.AppendFormat(Globals.templateStrings._spawnOrginCommands,
												-dpd.metersPerTick * dpd.ticksStartOffset,
												difficultyNumber);

				spawnNotesBaseCommands.AppendFormat(Globals.templateStrings._spawnNotesBaseCommand,
													difficultyNumber,
													dpd.folder_uuid,
													difficultyName);

				CreateDifficultyDisplay(songDifficultyID, difficultyName, dpd.folder_uuid, ref difficultyDisplayCommands);


				// Write difficulty-specific-file commands
				string playCommands = string.Format(Globals.templateStrings._playCommands,
													difficultyNumber,
													dpd.folder_uuid);
				string playPath = Path.Combine(dpd.folder_uuidFunctionsPath, difficultyName + "_play" + Globals.C_McFunction);
				SafeFileManagement.SetFileContents(playPath, playCommands);


				string playSongCommand = string.Format(Globals.templateStrings._playSongCommand,
														dpd.ticksStartOffset - 1,
														dpd.folder_uuid);
				string commandBasePath = Path.Combine(dpd.folder_uuidFunctionsPath, difficultyName + Globals.C_McFunction);
				SafeFileManagement.AppendFile(commandBasePath, playSongCommand);

				string completedSongCommand = string.Format(Globals.templateStrings._completedSong,
															songDifficultyID);
				string completedSongPath = Path.Combine(dpd.folder_uuidFunctionsPath, Globals.C_MapDifficultyCompleted);
				SafeFileManagement.AppendFile(completedSongPath, completedSongCommand);

				// Generate main note/obsicle data
				GenerateNotes(song, difficultyName, commandBasePath, packInfo, dpd);
				GenerateObsicles(song, difficultyName, commandBasePath, packInfo, dpd);

				difficultyNumber++;
			}

			// Write collected commands to files
			string difficultiesFunctionPath = Path.Combine(dpd.folder_uuidFunctionsPath, Globals.C_Difficulties);
			string initFunctionPath = Path.Combine(dpd.folder_uuidFunctionsPath, Globals.C_InitFunction);
			string setSpawnOrginFunctionPath = Path.Combine(dpd.folder_uuidFunctionsPath, Globals.C_SetSpawnOrgin);

			SafeFileManagement.AppendFile(dpd.spawnNotesBasePath, spawnNotesBaseCommands.ToString());
			SafeFileManagement.AppendFile(setSpawnOrginFunctionPath, spawnOrginCommands.ToString());
			SafeFileManagement.AppendFile(initFunctionPath, scoreboardCommands.ToString());

			// Add back button in tellraw
			difficultyDisplayCommands.Append(Globals.templateStrings._mainMenuBack);
			SafeFileManagement.AppendFile(difficultiesFunctionPath, difficultyDisplayCommands.ToString());
		}

		/// <summary>
		/// Generate note commands given a song and difficulty
		/// </summary>
		/// <param name="song">Beatmap data for a song and difficulty</param>
		/// <param name="difficultyName">Minecraft safe difficulty name</param>
		/// <param name="commandBasePath">Base folder path to generate new mcfunctions</param>
		public static void GenerateNotes(BeatMapSong song, string difficultyName, string commandBasePath, PackInfo packInfo, DataPackData dpd)
		{
			_notes[] notes = song.beatMapData._notes;

			double prevNodeTime = 0;
			int nodeRowID = 1;
			int currentLevel = 1;
			int currentTick = 0;
			int prevCurrentTick = 0;
			int currentNumberOfCommands = 0;
			int noteIndex = 0;
			int currentCommandLimit = Globals.C_CommandLimit;

			// Main note generation
			while (noteIndex < notes.Length)
			{
				string commandLevelName = difficultyName + Globals.C_LvlNoteName + currentLevel;
				string commandLevelFileName = commandLevelName + Globals.C_McFunction;
				string commandLevelFilePath = Path.Combine(dpd.folder_uuidFunctionsPath, commandLevelFileName);
				StringBuilder currentCommands = new StringBuilder();
				// Continue to generate commands until all nodes and obsicles have been itterated though
				while (noteIndex < notes.Length && currentNumberOfCommands < currentCommandLimit)
				{
					if (prevNodeTime != notes[noteIndex]._time)
					{
						prevNodeTime = notes[noteIndex]._time;
						nodeRowID++;
					}

					NodeDataToCommands(notes[noteIndex], packInfo._beatsPerMinute, dpd.metersPerTick, nodeRowID, ref currentCommands, ref currentTick);

					prevNodeTime = notes[noteIndex]._time;
					currentNumberOfCommands += 3;
					noteIndex++;
				}

				if (noteIndex >= notes.Length)
				{
					currentTick += (int)(dpd.ticksStartOffset + 1); ;
					currentCommands.AppendFormat(Globals.templateStrings._finishedNotes,
												currentTick);
				}

				SafeFileManagement.SetFileContents(commandLevelFilePath, currentCommands.ToString());
				string baseCommand = string.Format(Globals.templateStrings._baseCommand,
													prevCurrentTick,
													currentTick,
													dpd.folder_uuid,
													commandLevelName);
				SafeFileManagement.AppendFile(commandBasePath, baseCommand);
				prevCurrentTick = currentTick + 1;
				currentCommandLimit = currentNumberOfCommands + Globals.C_CommandLimit;
				currentLevel++;
			}

			// Note pretty buy create a command if no obsicles are present in a map
			if (notes.Length == 0)
			{
				currentTick += (int)(dpd.ticksStartOffset + 1); ;
				string commandLevelName = difficultyName + Globals.C_LvlNoteName + currentLevel;
				string commandLevelFileName = commandLevelName + Globals.C_McFunction;
				string commandLevelFilePath = Path.Combine(dpd.folder_uuidFunctionsPath, commandLevelFileName);
				StringBuilder currentCommands = new StringBuilder();
				SafeFileManagement.SetFileContents(commandLevelFilePath, currentCommands.ToString());
				string baseCommand = string.Format(Globals.templateStrings._baseCommand,
													prevCurrentTick,
													currentTick,
													dpd.folder_uuid,
													commandLevelName);
				SafeFileManagement.AppendFile(commandBasePath, baseCommand);
				currentCommands.AppendFormat(Globals.templateStrings._finishedNotes,
											currentTick);
			}
		}

		/// <summary>
		/// Generate obsicles commands given a song and difficulty
		/// </summary>
		/// <param name="song">Beatmap data for a song and difficulty</param>
		/// <param name="difficultyName">Minecraft safe difficulty name</param>
		/// <param name="commandBasePath">Base folder path to generate new mcfunctions</param>
		public static void GenerateObsicles(BeatMapSong song, string difficultyName, string commandBasePath,PackInfo packInfo, DataPackData dpd)
		{
			_obstacles[] obstacles = song.beatMapData._obstacles;
			int obsicleIndex = 0;
			int currentLevel = 1;
			int currentTick = 0;
			int maxTick = 0;
			int minTick = -1;
			int prevCurrentTick = 0;
			int currentNumberOfCommands = 0;
			int currentCommandLimit = Globals.C_CommandLimit;

			// Main note generation
			while (obsicleIndex < obstacles.Length)
			{
				string commandLevelName = difficultyName + Globals.C_LvlObsicleName + currentLevel;
				string commandLevelFileName = commandLevelName + Globals.C_McFunction;
				string commandLevelFilePath = Path.Combine(dpd.folder_uuidFunctionsPath, commandLevelFileName);
				StringBuilder currentCommands = new StringBuilder();
				int maxNewTick = 0;
				int minNewTick = 0;
				minTick = -1;

				// Continue to generate commands until all nodes and obsicles have been itterated though
				while (obsicleIndex < obstacles.Length && currentNumberOfCommands < currentCommandLimit)
				{
					ObsicleDataToCommands(obstacles[obsicleIndex], packInfo._beatsPerMinute, dpd.metersPerTick, ref currentCommands, ref currentNumberOfCommands, ref minNewTick, ref maxNewTick);
					if (minTick == -1)
						minTick = minNewTick;

					maxTick = Mathf.Max(maxTick, maxNewTick);
					obsicleIndex++;
				}

				if (obsicleIndex >= obstacles.Length)
				{
					maxTick += (int)(dpd.ticksStartOffset + 1);
					currentCommands.AppendFormat(Globals.templateStrings._finishedObsicles,
												maxTick);
					if (minTick == -1)
						minTick = maxTick - 1;
				}

				SafeFileManagement.SetFileContents(commandLevelFilePath, currentCommands.ToString());
				string baseCommand = string.Format(Globals.templateStrings._baseCommand,
													minTick,
													maxTick,
													dpd.folder_uuid,
													commandLevelName);
				SafeFileManagement.AppendFile(commandBasePath, baseCommand);
				prevCurrentTick = currentTick + 1;
				currentCommandLimit = currentNumberOfCommands + Globals.C_CommandLimit;
				minTick = 0;
				maxTick = 0;
				currentLevel++;
			}

			// Note pretty buy create a command if no obsicles are present in a map
			if (obstacles.Length == 0)
			{
				string commandLevelName = difficultyName + Globals.C_LvlObsicleName + currentLevel;
				string commandLevelFileName = commandLevelName + Globals.C_McFunction;
				string commandLevelFilePath = Path.Combine(dpd.folder_uuidFunctionsPath, commandLevelFileName);
				StringBuilder currentCommands = new StringBuilder();
				currentTick++;
				currentCommands.AppendFormat(Globals.templateStrings._finishedObsicles,
											currentTick);
				SafeFileManagement.SetFileContents(commandLevelFilePath, currentCommands.ToString());
				string baseCommand = string.Format(Globals.templateStrings._baseCommand,
													minTick,
													maxTick + (int)(dpd.ticksStartOffset + 1),
													dpd.folder_uuid,
													commandLevelName);
				SafeFileManagement.AppendFile(commandBasePath, baseCommand);
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
		public static void ObsicleDataToCommands(_obstacles obstacle, float bpm, double metersPerTick, ref StringBuilder commandList, ref int addToNumberOfCommands, ref int minWholeTick, ref int maxWholeTick)
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
						commandList.AppendFormat(Globals.templateStrings._positionCommand,
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
		public static void NodeDataToCommands(_notes node, float bpm, double metersPerTick, int nodeRowID, ref StringBuilder commandList, ref int wholeTick)
		{
			//_lineIndex = col
			//_lineLayer = row

			double beatsPerTick = bpm / 60.0d / 20;
			double exactTick = node._time / beatsPerTick;

			wholeTick = (int)Mathf.Floor((float)exactTick);
			double fractionTick = exactTick % beatsPerTick;
			double fractionMeters = fractionTick * metersPerTick;

			commandList.AppendFormat(Globals.templateStrings._nodePositionCommand,
										wholeTick,
										node._lineIndex * 0.3d,
										node._lineLayer * 0.3d,
										-fractionMeters);
			commandList.AppendFormat(Globals.templateStrings._scoreCommand,
										wholeTick,
										nodeRowID);
			commandList.AppendFormat(Globals.templateStrings._nodeTypeCommand,
										wholeTick,
										Globals.noteTypes[node._type][node._cutDirection]);
		}

		/// <summary>
		/// Generate tellraw commands that displays a difficulty to the user
		/// </summary>
		/// <param name="modeID">NOTSURE</param>
		/// <param name="difficultyName">Name of the difficulty this is for</param>
		/// <param name="folderID"></param>
		/// <param name="commandList">String Builder object to append to</param>
		public static void CreateDifficultyDisplay(string modeID, string difficultyName, string folderID, ref StringBuilder commandList)
		{
			string difficultyCommand1 = Globals.templateStrings._difficultyChat;
			difficultyCommand1 = difficultyCommand1.Replace("DIFFNAME", modeID);
			difficultyCommand1 = difficultyCommand1.Replace("VALUE", "1");
			difficultyCommand1 = difficultyCommand1.Replace("DIFFICULTY", difficultyName);
			difficultyCommand1 = difficultyCommand1.Replace("COLOR", "green");
			difficultyCommand1 = difficultyCommand1.Replace("folder_uuid", folderID);

			string difficultyCommand2 = Globals.templateStrings._difficultyChat;
			difficultyCommand2 = difficultyCommand2.Replace("DIFFNAME", modeID);
			difficultyCommand2 = difficultyCommand2.Replace("VALUE", "0");
			difficultyCommand2 = difficultyCommand2.Replace("DIFFICULTY", difficultyName);
			difficultyCommand2 = difficultyCommand2.Replace("COLOR", "red");
			difficultyCommand2 = difficultyCommand2.Replace("folder_uuid", folderID);

			commandList.Append(difficultyCommand1);
			commandList.Append(difficultyCommand2);
		}
	}
}

