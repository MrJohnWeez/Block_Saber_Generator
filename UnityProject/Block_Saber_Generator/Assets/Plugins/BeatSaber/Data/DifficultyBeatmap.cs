using UnityEngine;
using System;

namespace BeatSaber.Data
{
    [Serializable]
    public class DifficultyBeatmap
    {
        /// <summary> Difficulty of map </summary>
        /// <remarks>
        /// <list type="number"> OneSaber
        /// <item> Easy </item>
        /// <item> Normal </item>
        /// <item> Hard </item>
        /// <item> Expert </item>
        /// <item> ExpertPlus </item>
        /// </list>
        /// </remarks>
        [SerializeField] private string _difficulty;
        /// <summary> Difficulty Rank (0-9) </summary>
        [SerializeField] private int _difficultyRank;
        /// <summary> Beatmap Filename </summary>
        /// <remarks>
        /// This is the local location to the difficulty file,
        /// which contains the difficulty's notes, obstacles, and lighting events.
        /// </remarks>
        [SerializeField] private string _beatmapFilename;
        /// <summary> Note Jump Movement Speed </summary>
        /// /// <remarks>
        /// Velocity of objects approaching the player, in meters per second
        /// <para> This is used, along with the defined BPM of the song,
        /// to calculate 2 very important values, called Jump Duration and Jump Distance. </para>
        /// <list type="bullet">
        /// <item> Jump Duration is the amount of beats where objects can be active. </item>
        /// <item> Jump Distance is the total amount of distance that objects need to travel within that Jump Duration. </item>
        /// </list>
        /// The Player rests in the exact middle of both of these values,
        /// so most mappers find it more convenient to have Half Jump Distance and Half Jump Duration.
        /// /// <list type="bullet">
        /// <item> Half Jump Distance is the distance from the Player that objects spawn.
        /// Some mappers refer to this as the "Spawn Point". </item>
        /// <item> Half Jump Duration is the amount of beats that is needed to reach the Player.
        /// It is also the amount of beats, forward in time, where objects spawn </item>
        /// </list>
        /// </remarks>
        [SerializeField] private float _noteJumpMovementSpeed;
        /// <summary> Note Jump Start Beat Offset. This value acts as a direct offset to the Half Jump Duration </summary>
        [SerializeField] private float _noteJumpStartBeatOffset;
        /// <summary> Custom data scoped to a single difficulty </summary>
        /// <remarks>
        /// This is an optional field that contains data unrelated to the official Beat Saber level format.
        /// If no custom data exists, this object should be removed entirely.
        /// <para> The exact specifics of what goes in _customData is entirely dependent on community-created content that needs them.As such,
        /// we cannot list all _customData fields here.
        /// You will have to do your own searching throughout the
        /// Beat Saber community to find map editors, tools, or mods that use this _customData object </para>
        /// </remarks>
        [SerializeField] private CustomBeatMapData _customData;


        /// <inheritdoc cref="_difficulty" />
        public string Difficulty { get => _difficulty; set => _difficulty = value; }

        /// <inheritdoc cref="_difficultyRank" />
        public int DifficultyRank { get => _difficultyRank; set => _difficultyRank = value; }

        /// <inheritdoc cref="_beatmapFilename" />
        public string BeatmapFilename { get => _beatmapFilename; set => _beatmapFilename = value; }

        /// <inheritdoc cref="_noteJumpMovementSpeed" />
        public float NoteJumpMovementSpeed { get => _noteJumpMovementSpeed; set => _noteJumpMovementSpeed = value; }

        /// <inheritdoc cref="_noteJumpStartBeatOffset" />
        public float NoteJumpStartBeatOffset { get => _noteJumpStartBeatOffset; set => _noteJumpStartBeatOffset = value; }

        /// <inheritdoc cref="_customData" />
        public CustomBeatMapData CustomData { get => _customData; set => _customData = value; }
    }
}
