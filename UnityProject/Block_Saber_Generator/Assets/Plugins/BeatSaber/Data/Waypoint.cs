using System;
using UnityEngine;

namespace BeatSaber.Data
{
    /// <summary> This is used to control BTS TinyTAN figures.
    /// More infomation can be found here:
    /// <para> https://docs.google.com/spreadsheets/d/1spW7LS-RvenLQBVXJl9w_iOwqr9r_ozxYo3JUlXq9Lc/edit#gid=0 </para>
    /// </summary>
    [Serializable]
    public class Waypoint
    {
        /// <summary> Time, in beats, where this object reaches the player </summary>
        [SerializeField] private float _time;
        /// <summary> An integer number, from 0 to 3,
        /// which represents the column where this note is located.
        /// The far left column is located at index 0,
        /// and increases to the far right column located at index 3
        /// </summary>
        [SerializeField] private int _lineIndex;
        /// <summary> An integer number, from 0 to 2,
        /// which represents the layer where this note is located.
        /// The bottommost layer is located at layer 0, 
        /// and inceases to the topmost layer located at index 2.
        /// </summary>
        [SerializeField] private int _lineLayer;
        /// <summary> Offset Direction (0-9) </summary>
        [SerializeField] private int _offsetDirection;


        /// <inheritdoc cref="_time" />
        public float Time { get => _time; set => _time = value; }

        /// <inheritdoc cref="_lineIndex" />
        public int LineIndex { get => _lineIndex; set => _lineIndex = value; }

        /// <inheritdoc cref="_lineLayer" />
        public int LineLayer { get => _lineLayer; set => _lineLayer = value; }

        /// <inheritdoc cref="_offsetDirection" />
        public int OffsetDirection { get => _offsetDirection; set => _offsetDirection = value; }
    }
}
