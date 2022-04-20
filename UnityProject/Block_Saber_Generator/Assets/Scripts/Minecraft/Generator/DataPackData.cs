using System.Collections.Generic;
using System.IO;
using System.Text;
using BeatSaber;
using UnityEngine;
using Utilities;
using Utilities.Wrappers;

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
        public string songGuid;
        public string datapackRootPath;
        public string fullOutputPath;
        public string blockSaberBaseFunctionsPath;
        public string folder_uuidFunctionsPath;
        public string spawnNotesBasePath;
        public double metersPerTick;
        public double ticksStartOffset;


        public DataPackData(string unzippedFolderPath, string datapackOutputPath, BeatSaberMap beatSaberMap)
        {
            var packInfo = beatSaberMap.InfoData;
            keyVars = new Dictionary<string, string>();
            folder_uuid = SafeFileManagement.GetFileName(Path.GetFileName(unzippedFolderPath)).MakeMinecraftSafe();
            packName = Globals.DATAPACK + folder_uuid;
            songGuid = beatSaberMap.GuidId.ToString();

            // Paths
            datapackRootPath = Path.Combine(unzippedFolderPath, packName);
            fullOutputPath = Path.Combine(datapackOutputPath, packName + Globals.ZIP);
            blockSaberBaseFunctionsPath = Path.Combine(datapackRootPath, Globals.DATA, Globals.BLOCK_SABER_BASE, Globals.FUNCTIONS);
            folder_uuidFunctionsPath = Path.Combine(datapackRootPath, Globals.DATA, folder_uuid, Globals.FUNCTIONS);
            spawnNotesBasePath = Path.Combine(folder_uuidFunctionsPath, Globals.SPAWN_NOTES_BASE_FUNCTION);

            // Values
            metersPerTick = packInfo.BeatsPerMinute / 60.0d * 24 * 0.21 / 20;
            ticksStartOffset = (int)(Mathf.Clamp((float)(packInfo.BeatsPerMinute / 60d * 10), 7, 20) / metersPerTick);

            // Set up Keys
            keyVars["MAPPER_NAME"] = packInfo.LevelAuthorName;
            keyVars["BEATS_PER_MINUTE"] = packInfo.BeatsPerMinute.ToString();
            keyVars["SONGID"] = beatSaberMap.GuidId.GetHashCode().ToString();
            keyVars["MOVESPEED"] = metersPerTick.ToString();
            keyVars["SONGTITLE"] = packInfo.SongName + " " + packInfo.SongSubName;
            keyVars["SONGSHORTNAME"] = packInfo.SongName;
            keyVars["SONGARTIST"] = packInfo.SongAuthorName;
            keyVars["folder_uuid"] = folder_uuid;
            keyVars["SONGDIFFICULTYID"] = songGuid + "1";

            StringBuilder listOfDifficulties = new StringBuilder();
            var beatMapSets = beatSaberMap.InfoData.DifficultyBeatmapSets;
            for (int beatMapCounts = 0; beatMapCounts < beatMapSets.Length; beatMapCounts++)
            {
                var beatMapInfos = beatMapSets[beatMapCounts].DifficultyBeatmaps;
                int beatMapCount = beatMapInfos.Length;
                for (int difficulty = 0; difficulty < beatMapCount; difficulty++)
                {
                    listOfDifficulties.Append(beatMapInfos[difficulty].Difficulty);
                    if (difficulty < beatMapCount - 1)
                    {
                        listOfDifficulties.Append(" | ");
                    }
                }
            }
            keyVars["DIFFICULTYLIST"] = listOfDifficulties.ToString();
        }
    }
}
