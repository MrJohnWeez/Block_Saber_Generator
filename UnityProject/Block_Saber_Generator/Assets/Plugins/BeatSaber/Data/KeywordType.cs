using System;
using UnityEngine;

namespace BeatSaber.Data
{
    /// <summary> Special Events Keyword Filters </summary>
    [Serializable]
    public class KeywordType
    {
        /// <summary> Keyword </summary>
        [SerializeField] private string _keyword;
        /// <summary> Special event </summary>
        [SerializeField] private int[] _specialEvents;


        /// <inheritdoc cref="_keyword" />
        public string Keyword { get => _keyword; set => _keyword = value; }

        /// <inheritdoc cref="_specialEvents" />
        public int[] SpecialEvents { get => _specialEvents; set => _specialEvents = value; }
    }
}
