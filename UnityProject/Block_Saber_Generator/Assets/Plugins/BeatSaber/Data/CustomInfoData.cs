using UnityEngine;
using System;

namespace BeatSaber.Data
{
    /// <summary> Extra data about beat map used for mods or descriptions </summary>
    [Serializable]
    public class CustomInfoData
    {
        /// <summary> Array of Beatmap Contributors </summary>
        [SerializeField] private Contributor[] _contributors;
        /// <summary> Custom Environment Name </summary>
        [SerializeField] private string _customEnvironment;
        /// <summary> Used to match platforms on modelsaber.com </summary>
        [SerializeField] private string _customEnvironmentHash;
        /// <summary> Editor used to create map </summary>
        [SerializeField] private MapEditor _editors;


        /// <inheritdoc cref="_contributors" />
        public Contributor[] Contributors { get => _contributors; set => _contributors = value; }

        /// <inheritdoc cref="_customEnvironment" />
        public string CustomEnvironment { get => _customEnvironment; set => _customEnvironment = value; }

        /// <inheritdoc cref="_editors" />
        public MapEditor Editors { get => _editors; set => _editors = value; }

        /// <inheritdoc cref="_customEnvironmentHash" />
        public string CustomEnvironmentHash { get => _customEnvironmentHash; set => _customEnvironmentHash = value; }
    }
}
