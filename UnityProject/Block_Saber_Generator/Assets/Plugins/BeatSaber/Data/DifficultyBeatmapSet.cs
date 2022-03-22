using UnityEngine;
using System;

namespace BeatSaber.Data
{
    /// <summary> Beatmap description </summary>
    [Serializable]
    public class DifficultyBeatmapSet
    {
        /// <summary> Beatmap Characteristic Name </summary>
        /// <remarks>
        /// <list type="number"> Standard
        /// <item> OneSaber </item>
        /// <item> NoArrows </item>
        /// <item> Lightshow </item>
        /// <item> Lawless </item>
        /// <item> 360Degree </item>
        /// <item> 90Degree </item>
        /// </list>
        /// </remarks>
        [SerializeField] private string _beatmapCharacteristicName;
        /// <summary> Beat map infos </summary>
        [SerializeField] private DifficultyBeatmap[] _difficultyBeatmaps;


        /// <inheritdoc cref="_beatmapCharacteristicName" />
        public string BeatmapCharacteristicName { get => _beatmapCharacteristicName; set => _beatmapCharacteristicName = value; }

        /// <inheritdoc cref="_difficultyBeatmaps" />
        public DifficultyBeatmap[] DifficultyBeatmaps { get => _difficultyBeatmaps; set => _difficultyBeatmaps = value; }
    }
}