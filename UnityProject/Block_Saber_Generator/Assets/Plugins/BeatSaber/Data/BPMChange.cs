using System;
using UnityEngine;

namespace BeatSaber.Data
{
    /// <summary> Beats per minute changes </summary>
    [Serializable]
    public class BPMChange
    {
        /// <summary> Time offset in beats of change </summary>
        [SerializeField] private float _time;
        /// <summary> Beats per minute </summary>
        [SerializeField] private float _BPM;
        /// <summary> Beats per bar </summary>
        [SerializeField] private int _beatsPerBar;
        /// <summary> Metronome Offset </summary>
        [SerializeField] private int _metronomeOffset;


        /// <inheritdoc cref="_time" />
        public float Time { get => _time; set => _time = value; }

        /// <inheritdoc cref="_BPM" />
        public float BPM { get => _BPM; set => _BPM = value; }

        /// <inheritdoc cref="_beatsPerBar" />
        public int BeatsPerBar { get => _beatsPerBar; set => _beatsPerBar = value; }

        /// <inheritdoc cref="_metronomeOffset" />
        public int MetronomeOffset { get => _metronomeOffset; set => _metronomeOffset = value; }
    }
}
