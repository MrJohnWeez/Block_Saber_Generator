using System;
using UnityEngine;

namespace BeatSaber.Data
{
    /// <summary> Custom red green blue color </summary>
    [Serializable]
    public class CustomColor
    {
        /// <summary> Red </summary>
        [SerializeField] private float r;
        /// <summary> Green </summary>
        [SerializeField] private float g;
        /// <summary> Blue </summary>
        [SerializeField] private float b;


        /// <inheritdoc cref="r" />
        public float R { get => r; set => r = value; }

        /// <inheritdoc cref="g" />
        public float G { get => g; set => g = value; }

        /// <inheritdoc cref="b" />
        public float B { get => b; set => b = value; }
    }
}
