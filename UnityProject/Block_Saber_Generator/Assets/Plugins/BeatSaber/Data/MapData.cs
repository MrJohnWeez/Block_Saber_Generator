using System;
using UnityEngine;

namespace BeatSaber.Data
{
    /// <summary> Beat map data for difficulty </summary>
    [Serializable]
    public class MapData
    {
        /// <summary> Beatmap Version </summary>
        [SerializeField] private string _version;
        /// <summary> Environment and lighting events </summary>
        [SerializeField] private Event[] _events;
        /// <summary> Notes and Bombs </summary>
        [SerializeField] private Note[] _notes;
        /// <summary> Walls </summary>
        [SerializeField] private Obstacle[] _obstacles;
        /// <summary> Beat map infos </summary>
        [SerializeField] private Waypoint[] _waypoints;
        /// <summary> Special Events Keyword Filters </summary>
        [SerializeField] private KeywordType[] _keyword;
        /// <summary> Difficulty custom data </summary>
        [SerializeField] private CustomMapData _customData;


        /// <inheritdoc cref="_version" />
        public string Version { get => _version; set => _version = value; }

        /// <inheritdoc cref="_events" />
        public Event[] Events { get => _events; set => _events = value; }

        /// <inheritdoc cref="_notes" />
        public Note[] Notes { get => _notes; set => _notes = value; }

        /// <inheritdoc cref="_obstacles" />
        public Obstacle[] Obstacles { get => _obstacles; set => _obstacles = value; }

        /// <inheritdoc cref="_waypoints" />
        public Waypoint[] Waypoints { get => _waypoints; set => _waypoints = value; }

        /// <inheritdoc cref="_specialEventsKeywordFilters" />
        public KeywordType[] Keyword { get => _keyword; set => _keyword = value; }

        /// <inheritdoc cref="_customData" />
        public CustomMapData CustomData { get => _customData; set => _customData = value; }
    }
}
