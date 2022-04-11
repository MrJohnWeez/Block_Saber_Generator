using System;
using UnityEngine;

namespace BeatSaber.Data
{
    /// <summary> Custom map data </summary>
    [Serializable]
    public class CustomMapData
    {
        /// <summary> Time offset in beats of change </summary>
        [SerializeField] private string _time;
        /// <summary> Time in beats for beats per minute changes </summary>
        [SerializeField] private BPMChange[] _BPMChanges;
        /// <summary> Environment changes </summary>
        [SerializeField] private Environment _environment;


        /// <inheritdoc cref="_time" />
        public string Time { get => _time; set => _time = value; }

        /// <inheritdoc cref="_BPMChanges" />
        public BPMChange[] BPMChanges { get => _BPMChanges; set => _BPMChanges = value; }

        /// <inheritdoc cref="_environment" />
        public Environment Environment { get => _environment; set => _environment = value; }
    }
}
