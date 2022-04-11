using System;
using UnityEngine;

namespace BeatSaber.Data
{
    /// <summary> Environment / lighting event custom data </summary>
    [Serializable]
    public class CustomEventData
    {
        /// <summary> Color </summary>
        [SerializeField] private float[] _color;

        /// <summary> Light ID </summary>
        [SerializeField] private float[] _lightID;


        /// <inheritdoc cref="_color" />
        public float[] Color { get => _color; set => _color = value; }

        /// <inheritdoc cref="_lightID" />
        public float[] LightID { get => _lightID; set => _lightID = value; }
    }
}
