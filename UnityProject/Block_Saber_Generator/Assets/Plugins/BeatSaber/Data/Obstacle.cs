using System;
using UnityEngine;

namespace BeatSaber.Data
{
    /// <summary> Wall </summary>
    [Serializable]
    public class Obstacle
    {
        /// <summary> Time, in beats, where this object reaches the player </summary>
        [SerializeField] private float _time;
        /// <summary> An integer number, from 0 to 3,
        /// which represents the column where this note is located.
        /// The far left column is located at index 0,
        /// and increases to the far right column located at index 3
        /// </summary>
        [SerializeField] private int _lineIndex;
        /// <summary> An integer number which represents the state of the obstacle. </summary>
        /// <remarks>
        /// <list type="number"> Full height wall (0)
        /// <item> Crouch/duck wall (1) </item>
        /// </list>
        /// </remarks>
        [SerializeField] private int _type;
        /// <summary> The time, in beats, that the obstacle extends for. </summary>
        /// <remarks> 
        /// While _duration can go into negative numbers, be aware that this has some unintended effects 
        /// </remarks>
        [SerializeField] private int _duration;
        /// <summary> How many columns the obstacle takes up. </summary>
        /// <remarks> 
        /// A _width of 4 will mean that this wall will extend the entire playable grid.
        /// While _width can go into negative numbers, be aware that this has some unintended effects.
        /// </remarks> 
        [SerializeField] private int _width;


        /// <inheritdoc cref="_time" />
        public float Time { get => _time; set => _time = value; }

        /// <inheritdoc cref="_lineIndex" />
        public int LineIndex { get => _lineIndex; set => _lineIndex = value; }

        /// <inheritdoc cref="_type" />
        public int Type { get => _type; set => _type = value; }

        /// <inheritdoc cref="_duration" />
        public int Duration { get => _duration; set => _duration = value; }

        /// <inheritdoc cref="_width" />
        public int Width { get => _width; set => _width = value; }
    }
}
