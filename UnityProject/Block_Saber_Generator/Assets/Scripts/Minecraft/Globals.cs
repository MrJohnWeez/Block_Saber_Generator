using System.IO;
using UnityEngine;
using Utilities.Wrappers;

namespace Minecraft
{
    /// <summary>
    /// Static class for commonly used values
    /// </summary>
    public static class Globals
    {
        public const int NUMBER_OF_IO_RETRY_ATTEMPTS = 5;
        public const int COMMAND_LIMIT = 10000;

        // Extension names
        public static readonly string[] excludeExtensions = { ".meta" };
        public static readonly string[] excludeKeyVarExtensions = { ".meta", ".nbt" };
        public const string ZIP = ".zip";
        public const string OGG = ".ogg";
        public const string MCFUNCTION = ".mcfunction";

        // File Names
        public const string PACK_META = "pack.mcmeta";
        public const string PACK_ICON = "pack.png";
        public const string SOUNDS_JSON = "sounds.json";
        public const string SPAWN_NOTES_BASE_FUNCTION = "spawn_notes_base.mcfunction";
        public const string INIT_FUNCTION = "init.mcfunction";
        public const string DIFFICULTIES = "difficulties.mcfunction";
        public const string SET_SPAWN_ORIGIN = "set_spawn_orgin.mcfunction";
        public const string TEMPLATE_STRINGS = "TemplateStrings.json";
        public const string MAP_DIFFICULTY_COMPLETED = "map_difficulty_completed.mcfunction";
        public const string TICK = "tick.mcfunction";

        // Sub words
        public const string LEVEL_NOTE_NAME = "_lvl_note_";
        public const string LEVEL_OBSTACLE_NAME = "_lvl_obsicle_";
        public const string LEVEL_EVENT_NAME = "_lvl_event_";

        // Folder Names
        public const string MINECRAFT = "minecraft";
        public const string TEMPLATE_RESOURCES_PACK_NAME = @"SONG_AUTHOR - SONG_NAME";
        public const string TEMPLATE_DATA_PACK_NAME = "BlockSaberTemplate";
        public const string ASSETS = "assets";
        public const string SOUNDS = "sounds";
        public const string CUSTOM = "custom";
        public const string RESOURCEPACK = "ResourcePack_";
        public const string DATA = "data";
        public const string FUNCTIONS = "functions";
        public const string DATAPACK = "DataPack_";
        public const string BLOCK_SABER_BASE = "_root_class";
        public const string FOLDER_UUID = "folder_uuid";
        public static readonly string pathOfResourcepackTemplate = Path.Combine(ProcessManager.streamingAssets, "TemplateResourcepack");
        public static readonly string pathOfDatapackTemplate = Path.Combine(ProcessManager.streamingAssets, "TemplateDatapack");
        public static readonly string pathOfTemplateStrings = Path.Combine(ProcessManager.streamingAssets, TEMPLATE_STRINGS);
        public static readonly TemplateStrings templateStrings = JsonUtility.FromJson<TemplateStrings>(SafeFileManagement.GetFileContents(pathOfTemplateStrings));
        public static readonly string[] obstacleTypes = new string[4] {
                                                                        "wall_1x1",
                                                                        "wall_1x3",
                                                                        "wall_2x1",
                                                                        "wall_2x3"
                                                                    };
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