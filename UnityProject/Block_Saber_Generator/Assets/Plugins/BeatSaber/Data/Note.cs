using System;
using UnityEngine;

namespace BeatSaber.Data
{
    /// <summary> Notes or Bomb </summary>
    [Serializable]
    public class Note
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
        /// <summary> This indicates the type of note there is. </summary>
        /// <remarks>
        /// <list type="number"> Left (Red) Note (0)
        /// <item> Right (Blue) Note (1) </item>
        /// <item> Unused (2) </item>
        /// <item> Bomb (3) </item>
        /// </list>
        /// </remarks>
        [SerializeField] private int _type;
        /// <summary> This indicates the cut direction for the note </summary>
        /// <remarks>
        /// <list type="number"> Up (0)
        /// <item> Down (1) </item>
        /// <item> Left (2) </item>
        /// <item> Right (3) </item>
        /// <item> Up Left (4) </item>
        /// <item> Up Right (5) </item>
        /// <item> Down Left (6) </item>
        /// <item> Down Right (7) </item>
        /// <item> Any(Dot Note) (8) </item>
        /// </list>
        /// </remarks>
        [SerializeField] private int _cutDirection;


        /// <inheritdoc cref="_time" />
        public float Time { get => _time; set => _time = value; }

        /// <inheritdoc cref="_lineIndex" />
        public int LineIndex { get => _lineIndex; set => _lineIndex = value; }

        /// <inheritdoc cref="_lineLayer" />
        public int LineLayer { get => _lineLayer; set => _lineLayer = value; }

        /// <inheritdoc cref="_type" />
        public int Type { get => _type; set => _type = value; }

        /// <inheritdoc cref="_cutDirection" />
        public int CutDirection { get => _cutDirection; set => _cutDirection = value; }
    }
}
