using UnityEngine;
using System;

namespace BeatSaber.Data
{
    /// <summary> Beatmap Contributor who participated in beatmap creation </summary>
    [Serializable]
    public class Contributor
    {
        /// <summary> Contributor Role </summary>
        [SerializeField] private string _role;
        /// <summary> Contributor Name </summary>
        [SerializeField] private string _name;
        /// <summary> Contributor Icon Path </summary>
        [SerializeField] private string _iconPath;


        /// <inheritdoc cref="_role" />
        public string Role { get => _role; set => _role = value; }

        /// <inheritdoc cref="_name" />
        public string Name { get => _name; set => _name = value; }

        /// <inheritdoc cref="_iconPath" />
        public string IconPath { get => _iconPath; set => _iconPath = value; }
    }
}