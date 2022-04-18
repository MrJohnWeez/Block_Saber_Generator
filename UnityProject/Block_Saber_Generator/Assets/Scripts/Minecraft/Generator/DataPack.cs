using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BeatSaber;
using BeatSaber.Data;
using UnityEngine;
using Utilities;
using Utilities.Wrappers;

namespace Minecraft.Generator
{
    /// <summary>
    /// Class that allows for quick conversion of Beat Saber data to a Minecraft datapack
    /// </summary>
    public static class DataPack
    {
        public class EventFadeSave
        {
            public BeatSaber.Data.Event bEvent = null;
            public string eventName = "";
            public int tickCount = -1;

        }

        /// <summary>
        /// Generate a minecraft datapack from Beat Saber data
        /// </summary>
        /// <param name="unzippedFolderPath">Path of unzipped Beat Saber data</param>
        /// <param name="datapackOutputPath">Path to output datapack</param>
        /// <param name="packInfo">Beat Saber Parsed info</param>
        /// <param name="beatMapSongList">List of Beat Saber song data</param>
        /// <param name="cancellationToken">Token that allows async function to be canceled</param>
        /// <returns></returns>
        public static Task<int> FromBeatSaberData(string datapackOutputPath, BeatSaberMap beatSaberMap, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var unzippedFolderPath = beatSaberMap.ExtractedFilePath;
                if (!Directory.Exists(unzippedFolderPath))
                {
                    return 0;
                }
                DataPackData dataPackData = new DataPackData(unzippedFolderPath, datapackOutputPath, beatSaberMap);
                if (beatSaberMap.InfoData.DifficultyBeatmapSets.Length > 0)
                {
                    // Copying Template
                    string copiedTemplatePath = Path.Combine(unzippedFolderPath, Globals.C_TemplateDataPackName);
                    if (SafeFileManagement.DirectoryCopy(Globals.pathOfDatapackTemplate, unzippedFolderPath, true, Globals.excludeExtensions, Globals.C_numberOfIORetryAttempts))
                    {
                        try
                        {
                            if (SafeFileManagement.MoveDirectory(copiedTemplatePath, dataPackData.datapackRootPath, Globals.C_numberOfIORetryAttempts))
                            {
                                cancellationToken.ThrowIfCancellationRequested();

                                // Must change the folder names before searching for keys
                                string songname_uuidFolder = Path.Combine(dataPackData.datapackRootPath, Globals.C_Data, Globals.C_FolderUUID);
                                string newPath = Path.Combine(dataPackData.datapackRootPath, Globals.C_Data, dataPackData.folder_uuid);
                                SafeFileManagement.MoveDirectory(songname_uuidFolder, newPath, Globals.C_numberOfIORetryAttempts);

                                // Updating Copied files
                                Filemanagement.UpdateAllCopiedFiles(dataPackData.datapackRootPath, dataPackData.keyVars, true, Globals.excludeKeyVarExtensions);

                                // Copying Image Icon
                                string mapIcon = Path.Combine(unzippedFolderPath, beatSaberMap.InfoData.CoverImageFilename);
                                string packIcon = Path.Combine(dataPackData.datapackRootPath, Globals.C_PackIcon);
                                SafeFileManagement.CopyFileTo(mapIcon, packIcon, true, Globals.C_numberOfIORetryAttempts);

                                cancellationToken.ThrowIfCancellationRequested();

                                // Generating main datapack files
                                int errorCode = GenerateMCBeatData(beatSaberMap, dataPackData);
                                if (errorCode >= 0)
                                {
                                    return errorCode;
                                }
                                cancellationToken.ThrowIfCancellationRequested();

                                // Zipping files
                                Archive.Compress(dataPackData.datapackRootPath, dataPackData.fullOutputPath);
                                return -1;
                            }
                        }
                        catch (OperationCanceledException wasCanceled)
                        {
                            throw wasCanceled;
                        }
                        catch (ObjectDisposedException wasAreadyCanceled)
                        {
                            throw wasAreadyCanceled;
                        }
                    }
                }
                return 0;
            });
        }

        /// <summary>
        /// Main generation of minecraft commands for beat saber data
        /// </summary>
        /// <param name="beatMapSongList">List of Beat Saber song data</param>
        /// <param name="packInfo">Beat Saber Parsed info</param>
        /// <param name="dpd">Data used for datapack generation</param>
        /// <returns></returns>
        public static int GenerateMCBeatData(BeatSaberMap beatSaberMap, DataPackData dpd)
        {
            StringBuilder difficultyDisplayCommands = new StringBuilder();
            StringBuilder scoreboardCommands = new StringBuilder();
            StringBuilder spawnOriginCommands = new StringBuilder();
            StringBuilder spawnNotesBaseCommands = new StringBuilder();
            int difficultyNumber = 1;

            // Iterate though each song difficulty
            var mapDataInfos = beatSaberMap.MapDataInfos;
            foreach (var key in mapDataInfos.Keys)
            {
                var mapDataInfo = mapDataInfos[key];
                if (mapDataInfo.MapData.Notes.Length > 0 || mapDataInfo.MapData.Obstacles.Length > 0)
                {
                    string difficultyName = mapDataInfo.DifficultyBeatmapInfo.Difficulty.MakeMinecraftSafe();

                    // Append running command lists
                    string songDifficultyID = dpd.songGuid + difficultyNumber.ToString();
                    scoreboardCommands.AppendFormat(Globals.templateStrings._scoreboardCommand, songDifficultyID);

                    spawnOriginCommands.AppendFormat(Globals.templateStrings._spawnOriginCommands,
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
                                                                difficultyNumber,
                                                                songDifficultyID);
                    string completedSongPath = Path.Combine(dpd.folder_uuidFunctionsPath, Globals.C_MapDifficultyCompleted);
                    SafeFileManagement.AppendFile(completedSongPath, completedSongCommand);

                    // Generate main note/obstacle data/light data
                    GenerateNotes(mapDataInfo.MapData, difficultyName, commandBasePath, beatSaberMap.InfoData, dpd);
                    GenerateObstacles(mapDataInfo.MapData, difficultyName, commandBasePath, beatSaberMap.InfoData, dpd);
                    GenerateEvents(mapDataInfo.MapData, difficultyName, commandBasePath, beatSaberMap.InfoData, dpd);

                    difficultyNumber++;
                }
            }

            // Write collected commands to files
            string difficultiesFunctionPath = Path.Combine(dpd.folder_uuidFunctionsPath, Globals.C_Difficulties);
            string initFunctionPath = Path.Combine(dpd.folder_uuidFunctionsPath, Globals.C_InitFunction);
            string setSpawnOrginFunctionPath = Path.Combine(dpd.folder_uuidFunctionsPath, Globals.C_SetSpawnOrgin);

            SafeFileManagement.AppendFile(dpd.spawnNotesBasePath, spawnNotesBaseCommands.ToString());
            SafeFileManagement.AppendFile(setSpawnOrginFunctionPath, spawnOriginCommands.ToString());
            SafeFileManagement.AppendFile(initFunctionPath, scoreboardCommands.ToString());

            // Add back button in tellraw
            difficultyDisplayCommands.Append(Globals.templateStrings._mainMenuBack);
            SafeFileManagement.AppendFile(difficultiesFunctionPath, difficultyDisplayCommands.ToString());
            return -1;
        }

        public static void GenerateEvents(MapData song, string difficultyName, string commandBasePath, Info packInfo, DataPackData dpd)
        {
            int currentLevel = 1;
            int currentTick = 0;
            int prevCurrentTick = 0;
            int currentNumberOfCommands = 0;
            int noteIndex = 0;
            int currentCommandLimit = Globals.C_CommandLimit;

            var bEvents = song.Events.Where(x => x.Value >= 0 && x.Value <= 11).ToArray();

            List<EventFadeSave> autoOffTick = new List<EventFadeSave>();
            for (int i = 0; i < 10; i++)
            {
                autoOffTick.Add(new EventFadeSave());
            }
            // Main note generation
            while (noteIndex < bEvents.Length)
            {
                string commandLevelName = difficultyName + Globals.C_LvlEventName + currentLevel;
                string commandLevelFileName = commandLevelName + Globals.C_McFunction;
                string commandLevelFilePath = Path.Combine(dpd.folder_uuidFunctionsPath, commandLevelFileName);
                StringBuilder currentCommands = new StringBuilder();

                // Continue to generate commands until all events
                while (noteIndex < bEvents.Length && currentNumberOfCommands < currentCommandLimit)
                {
                    currentNumberOfCommands += EventDataToCommands(bEvents[noteIndex], packInfo.BeatsPerMinute, dpd, ref currentCommands, ref currentTick, ref autoOffTick);
                    noteIndex++;
                }

                if (noteIndex >= bEvents.Length)
                {
                    currentTick += (int)(dpd.ticksStartOffset + 1); ;
                    currentCommands.AppendFormat(Globals.templateStrings._finishedEvents, currentTick);
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


            // Note pretty buy create a command if no obstacles are present in a map
            if (bEvents.Length == 0)
            {
                currentTick += (int)(dpd.ticksStartOffset + 1); ;
                string commandLevelName = difficultyName + Globals.C_LvlEventName + currentLevel;
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
                currentCommands.AppendFormat(Globals.templateStrings._finishedNotes, currentTick);
            }
        }

        public static int EventDataToCommands(BeatSaber.Data.Event bEvent, float bpm, DataPackData dpd, ref StringBuilder commandList, ref int wholeTick, ref List<EventFadeSave> autoOffTick)
        {
            int valueType = bEvent.Value;
            string eventName;
            int typeType = bEvent.Type;
            int indexValue;
            int addedLines = 0;
            switch (typeType)
            {
                case 0:
                    eventName = "BackLasersGroup";
                    indexValue = 0;
                    break;
                case 1:
                    eventName = "RingLightsGroup";
                    indexValue = 1;
                    break;
                case 2:
                    eventName = "LeftRotatingLasersGroup";
                    indexValue = 2;
                    break;
                case 3:
                    eventName = "RightRotatingLasersGroup";
                    indexValue = 3;
                    break;
                case 4:
                    eventName = "CenterLightsGroup";
                    indexValue = 4;
                    break;
                case 5:
                    eventName = "BoostLight";
                    indexValue = 5;
                    break;
                case 6:
                    eventName = "ExtraLeftSideLights";
                    indexValue = 6;
                    break;
                case 7:
                    eventName = "ExtraRightSideLights";
                    indexValue = 7;
                    break;
                case 10:
                    eventName = "LeftSideLasers";
                    indexValue = 8;
                    break;
                case 11:
                    eventName = "RightSideLasers";
                    indexValue = 9;
                    break;
                default:
                    return addedLines;
            }

            double beatsPerTick = bpm / 60.0d / 20;
            double exactTick = bEvent.Time / beatsPerTick;
            wholeTick = (int)Mathf.Floor((float)exactTick) + (int)dpd.ticksStartOffset;

            for (int i = 0; i < autoOffTick.Count; i++)
            {
                if (autoOffTick[i].bEvent != null && autoOffTick[i].bEvent.Type != typeType && autoOffTick[i].tickCount >= 0 && autoOffTick[i].tickCount <= wholeTick)
                {
                    commandList.AppendFormat(Globals.templateStrings._eventOnOff, autoOffTick[i].tickCount, autoOffTick[i].eventName + "OnOff", 0);
                    commandList.AppendFormat(Globals.templateStrings._eventColor, autoOffTick[i].tickCount, autoOffTick[i].eventName + "Color", 0);
                    addedLines += 2;
                    Debug.Log("Added fade end");
                }
            }
            if ((valueType == 3 || valueType == 7))
            {
                autoOffTick[indexValue].tickCount = wholeTick + 40;
                autoOffTick[indexValue].bEvent = bEvent;
                autoOffTick[indexValue].eventName = eventName;
            }
            else
            {
                autoOffTick[indexValue].tickCount = -1;
                autoOffTick[indexValue].bEvent = null;
                autoOffTick[indexValue].eventName = "";
            }

            bool isLightOn = valueType != 0;
            int lightColor = (valueType > 0 && valueType <= 4) ? 16 : 0;

            commandList.AppendFormat(Globals.templateStrings._eventOnOff, wholeTick, eventName + "OnOff", isLightOn ? 1 : 0);
            commandList.AppendFormat(Globals.templateStrings._eventColor, wholeTick, eventName + "Color", lightColor);
            return addedLines + 2;
        }

        /// <summary>
        /// Generate note commands given a song and difficulty
        /// </summary>
        /// <param name="song">Beatmap data for a song and difficulty</param>
        /// <param name="difficultyName">Minecraft safe difficulty name</param>
        /// <param name="commandBasePath">Base folder path to generate new mcfunctions</param>
        /// <param name="packInfo">Beat Saber Parsed info</param>
        /// <param name="dpd">Data used for datapack generation</param>
        public static void GenerateNotes(MapData song, string difficultyName, string commandBasePath, Info packInfo, DataPackData dpd)
        {
            double prevNodeTime = 0;
            int nodeRowID = 1;
            int currentLevel = 1;
            int currentTick = 0;
            int prevCurrentTick = 0;
            int currentNumberOfCommands = 0;
            int noteIndex = 0;
            int currentCommandLimit = Globals.C_CommandLimit;

            var notes = song.Notes;
            // Main note generation
            while (noteIndex < notes.Length)
            {
                string commandLevelName = difficultyName + Globals.C_LvlNoteName + currentLevel;
                string commandLevelFileName = commandLevelName + Globals.C_McFunction;
                string commandLevelFilePath = Path.Combine(dpd.folder_uuidFunctionsPath, commandLevelFileName);
                StringBuilder currentCommands = new StringBuilder();

                // Continue to generate commands until all nodes and obstacles have been iterated though
                while (noteIndex < notes.Length && currentNumberOfCommands < currentCommandLimit)
                {
                    if (prevNodeTime != notes[noteIndex].Time)
                    {
                        prevNodeTime = notes[noteIndex].Time;
                        nodeRowID++;
                    }

                    NodeDataToCommands(notes[noteIndex], packInfo.BeatsPerMinute, dpd.metersPerTick, nodeRowID, ref currentCommands, ref currentTick);

                    prevNodeTime = notes[noteIndex].Time;
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

            // Note pretty buy create a command if no obstacles are present in a map
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
        /// Generate obstacles commands given a song and difficulty
        /// </summary>
        /// <param name="song">Beatmap data for a song and difficulty</param>
        /// <param name="difficultyName">Minecraft safe difficulty name</param>
        /// <param name="commandBasePath">Base folder path to generate new mcfunctions</param>
        /// <param name="packInfo">Beat Saber Parsed info</param>
        /// <param name="dpd">Data used for datapack generation</param>
        public static void GenerateObstacles(MapData song, string difficultyName, string commandBasePath, Info packInfo, DataPackData dpd)
        {
            Obstacle[] obstacles = song.Obstacles;
            int obstacleIndex = 0;
            int currentLevel = 1;
            int currentTick = 0;
            int maxTick = 0;
            int minTick = -1;
            int prevCurrentTick = 0;
            int currentNumberOfCommands = 0;
            int currentCommandLimit = Globals.C_CommandLimit;

            // Main note generation
            while (obstacleIndex < obstacles.Length)
            {
                string commandLevelName = difficultyName + Globals.C_LvlObstacleName + currentLevel;
                string commandLevelFileName = commandLevelName + Globals.C_McFunction;
                string commandLevelFilePath = Path.Combine(dpd.folder_uuidFunctionsPath, commandLevelFileName);
                StringBuilder currentCommands = new StringBuilder();
                int maxNewTick = 0;
                int minNewTick = 0;
                minTick = -1;

                // Continue to generate commands until all nodes and obsicles have been itterated though
                while (obstacleIndex < obstacles.Length && currentNumberOfCommands < currentCommandLimit)
                {
                    ObstacleDataToCommands(obstacles[obstacleIndex], packInfo.BeatsPerMinute, dpd.metersPerTick, ref currentCommands, ref currentNumberOfCommands, ref minNewTick, ref maxNewTick);
                    if (minTick == -1)
                    {
                        minTick = minNewTick;
                    }

                    maxTick = Mathf.Max(maxTick, maxNewTick);
                    obstacleIndex++;
                }

                if (obstacleIndex >= obstacles.Length)
                {
                    maxTick += (int)(dpd.ticksStartOffset + 1);
                    currentCommands.AppendFormat(Globals.templateStrings._finishedObstacles,
                                                maxTick);
                    if (minTick == -1)
                    {
                        minTick = maxTick - 1;
                    }
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

            // Note pretty buy create a command if no obstacles are present in a map
            if (obstacles.Length == 0)
            {
                string commandLevelName = difficultyName + Globals.C_LvlObstacleName + currentLevel;
                string commandLevelFileName = commandLevelName + Globals.C_McFunction;
                string commandLevelFilePath = Path.Combine(dpd.folder_uuidFunctionsPath, commandLevelFileName);
                StringBuilder currentCommands = new StringBuilder();
                currentTick++;
                currentCommands.AppendFormat(Globals.templateStrings._finishedObstacles,
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
        /// Generate commands to produce a beat saber obstacle
        /// </summary>
        /// <param name="obstacle">Obstacle data</param>
        /// <param name="bpm">Beats per minute</param>
        /// <param name="metersPerTick">Distance note travels per tick</param>
        /// <param name="commandList">String Builder object to append to</param>
        /// <param name="addToNumberOfCommands">Output of number of commands generated</param>
        /// <param name="minWholeTick">Output of min minecraft tick used</param>
        /// <param name="maxWholeTick">Output of max minecraft tick used</param>
        public static void ObstacleDataToCommands(Obstacle obstacle, float bpm, double metersPerTick, ref StringBuilder commandList, ref int addToNumberOfCommands, ref int minWholeTick, ref int maxWholeTick)
        {
            double beatsPerTick = bpm / 60.0d / 20;
            double exactTick = obstacle.Time / beatsPerTick;

            minWholeTick = (int)Math.Truncate(exactTick);
            double fractionTick = exactTick % beatsPerTick;
            double fractionMeters = fractionTick * metersPerTick;

            // Calculate the LWH of the rectangular prism to generate
            int lengthOfWallInNotes = (int)Math.Truncate((double)obstacle.Duration * 24);

            if (lengthOfWallInNotes == 0)
            {
                lengthOfWallInNotes++;
            }

            int widthOfWallInNotes = obstacle.Width;
            int heightOfWallInNotes = obstacle.Type == 0 ? 3 : 1;
            int isHeightTall = obstacle.Type == 0 ? 1 : 0;

            for (int length = 0; length < lengthOfWallInNotes; length++)
            {
                double meterLengths = fractionMeters + 0.21 * length;
                int tickOffset = meterLengths != 0 ? (int)Math.Truncate(meterLengths / metersPerTick) : 0;
                double extraOffset = meterLengths != 0 ? meterLengths % metersPerTick : 0;

                maxWholeTick = minWholeTick + tickOffset;

                // The code below generates red walls as optimized models (max of 4 entities per tick)
                if (widthOfWallInNotes == 1 || widthOfWallInNotes == 2)
                {
                    int obstacleType = widthOfWallInNotes == 1 ? isHeightTall : isHeightTall + 2;
                    commandList.AppendFormat(Globals.templateStrings._wallCommand,
                                            maxWholeTick,
                                            0.3 * obstacle.LineIndex,
                                            0.6d + -0.3 * isHeightTall,
                                            -extraOffset,
                                            Globals.obstacleTypes[obstacleType]);
                    addToNumberOfCommands += 2;
                }
                else if (widthOfWallInNotes == 3)
                {
                    commandList.AppendFormat(Globals.templateStrings._wallCommand,
                                            maxWholeTick,
                                            0.3 * obstacle.LineIndex,
                                            0.6d + -0.3 * isHeightTall,
                                            -extraOffset,
                                            Globals.obstacleTypes[isHeightTall + 2]);
                    commandList.AppendFormat(Globals.templateStrings._wallCommand,
                                            maxWholeTick,
                                            0.3 * (obstacle.LineIndex + 2),
                                            0.6d + -0.3 * isHeightTall,
                                            -extraOffset,
                                            Globals.obstacleTypes[isHeightTall]);
                    addToNumberOfCommands += 4;
                }
                else if (widthOfWallInNotes == 4)
                {
                    commandList.AppendFormat(Globals.templateStrings._wallCommand,
                                            maxWholeTick,
                                            0.3 * obstacle.LineIndex,
                                            0.6d + -0.3 * isHeightTall,
                                            -extraOffset,
                                            Globals.obstacleTypes[isHeightTall + 2]);
                    commandList.AppendFormat(Globals.templateStrings._wallCommand,
                                            maxWholeTick,
                                            0.3 * (obstacle.LineIndex + 2),
                                            0.6d + -0.3 * isHeightTall,
                                            -extraOffset,
                                            Globals.obstacleTypes[isHeightTall + 2]);
                    addToNumberOfCommands += 4;
                }
            }
        }

        /// <summary>
        /// Generate all necessary commands for node placement on beat
        /// </summary>
        /// <param name="note">Node data to use</param>
        /// <param name="bpm">Beats per minute</param>
        /// <param name="metersPerTick">Distance note travels per tick</param>
        /// <param name="nodeRowID">Row id within minecraft</param>
        /// <param name="commandList">String Builder object to append to</param>
        /// <param name="wholeTick">Output of minecraft tick used</param>
        public static void NodeDataToCommands(Note note, float bpm, double metersPerTick, int nodeRowID, ref StringBuilder commandList, ref int wholeTick)
        {
            //_lineIndex = col
            //_lineLayer = row
            int nodeType = note.Type;
            int nodeDir = note.CutDirection;

            nodeType = nodeType <= 3 ? nodeType : 0;
            nodeDir = nodeDir <= 8 ? nodeDir : 8;

            double beatsPerTick = bpm / 60.0d / 20;
            double exactTick = note.Time / beatsPerTick;

            wholeTick = (int)Mathf.Floor((float)exactTick);
            double fractionTick = exactTick % beatsPerTick;
            double fractionMeters = fractionTick * metersPerTick;

            commandList.AppendFormat(Globals.templateStrings._nodePositionCommand,
                                        wholeTick,
                                        note.LineIndex * 0.3d,
                                        note.LineLayer * 0.3d,
                                        -fractionMeters);
            commandList.AppendFormat(Globals.templateStrings._scoreCommand,
                                        wholeTick,
                                        nodeRowID);
            commandList.AppendFormat(Globals.templateStrings._nodeTypeCommand,
                                        wholeTick,
                                        Globals.noteTypes[nodeType][nodeDir]);
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

