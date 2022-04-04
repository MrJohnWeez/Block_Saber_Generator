using BeatSaber;
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
        public string folder_uuid;
        public string packName;
        public string uuid;
        public string datapackRootPath;
        public string fullOutputPath;
        public string blockSaberBaseFunctionsPath;
        public string folder_uuidFunctionsPath;
        public string spawnNotesBasePath;
        public double metersPerTick;
        public double ticksStartOffset;


        public DataPackData(string unzippedFolderPath, string datapackOutputPath, BeatSaberMap beatSaberMap)
        {
            var packInfo = beatSaberMap.Info;
            keyVars = new Dictionary<string, string>();
            folder_uuid = SafeFileManagement.GetFileName(Path.GetFileName(unzippedFolderPath)).MakeMinecraftSafe();
            packName = Globals.C_Datapack + folder_uuid;
            uuid = beatSaberMap.Guid.ToString("X");

            // Paths
            datapackRootPath = Path.Combine(unzippedFolderPath, packName);
            fullOutputPath = Path.Combine(datapackOutputPath, packName + Globals.C_Zip);
            blockSaberBaseFunctionsPath = Path.Combine(datapackRootPath, Globals.C_Data, Globals.C_BlockSaberBase, Globals.C_Functions);
            folder_uuidFunctionsPath = Path.Combine(datapackRootPath, Globals.C_Data, folder_uuid, Globals.C_Functions);
            spawnNotesBasePath = Path.Combine(folder_uuidFunctionsPath, Globals.C_SpawnNotesBaseFunction);

            // Values
            metersPerTick = packInfo.BeatsPerMinute / 60.0d * 24 * 0.21 / 20;
            ticksStartOffset = (int)(Mathf.Clamp((float)(packInfo.BeatsPerMinute / 60d * 10), 7, 20) / metersPerTick);

            // Set up Keys
            keyVars["MAPPER_NAME"] = packInfo.LevelAuthorName;
            keyVars["BEATS_PER_MINUTE"] = packInfo.BeatsPerMinute.ToString();
            keyVars["SONGID"] = uuid;
            keyVars["MOVESPEED"] = metersPerTick.ToString();
            keyVars["SONGTITLE"] = packInfo.SongName + " " + packInfo.SongSubName;
            keyVars["SONGSHORTNAME"] = packInfo.SongName;
            keyVars["SONGARTIST"] = packInfo.SongAuthorName;
            keyVars["folder_uuid"] = folder_uuid;

            string listOfDifficulties = "";
            var beatMapSets = beatSaberMap.Info.DifficultyBeatmapSets;
            for (int beatMapCount = 0; beatMapCount < beatMapSets.Length; beatMapCount++)
            {
                var beatMapInfos = beatMapSets[beatMapCount].DifficultyBeatmaps;
                for (int difficulty = 0; difficulty < beatMapInfos.Length; difficulty++)
                {
                    listOfDifficulties += beatMapInfos[difficulty].Difficulty + " | ";
                }
            }
            keyVars["DIFFICULTYLIST"] = listOfDifficulties;
        }
    }
}
