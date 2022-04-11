using UnityEngine;
using System;

namespace BeatSaber.Data
{
    /// <summary> 	Beat Saber Beatmap Info </summary>
    [Serializable]
    public class Info
    {
        /// <summary> Version of the map format </summary>
        [SerializeField] private string _version;
        /// <summary> Name of song used </summary>
        [SerializeField] private string _songName;
        /// <summary> Additional titles that could go into song </summary>
        /// <remarks> Examples:
        /// <list type="bullet">
        /// <item> Additional artists (Such as featured artists) </item>
        /// <item> Any variation in production(Song remix, VIP, etc.) </item>
        /// </list>
        /// </remarks>
        [SerializeField] private string _songSubName;
        /// <summary> Describes the main artist, group, band, brand, etc. for the song </summary>
        [SerializeField] private string _songAuthorName;
        /// <summary> Creator of Beatmap </summary>
        [SerializeField] private string _levelAuthorName;
        /// <summary> Beatmap BPM </summary>
        [SerializeField] private float _beatsPerMinute;
        /// <summary> Time (in beats) of how much a note should shift when shuffled </summary>
        /// <remarks>
        /// <para> If your song has "swing" in it, where some beats in a measure are intentionally offset from the rest,
        /// you can correct potential timing issues in your map by utilizing _shuffle.
        /// This indicates how far objects will move when they are determined to be on a swing beat. </para>
        /// <para> A positive value means they will be shifted forward in time,
        /// and a negative value means they will be shifted back in time.
        /// The total amount they will be offset by is described in _shufflePeriod,
        /// since they both work together to produce that value. </para>
        /// Uncommon in the community.
        /// </remarks>
        [SerializeField] private string _shuffle;
        /// <summary> Time (in beats) of how often a note should shift </summary>
        /// <remarks>
        /// <para> Used to determine when a swing beat will occur.
        /// More specifically, it is the time(in beats) where a swing beat will occur.
        /// But unfortunately, it's more complicated than this.
        /// Beat Saber alternates between a swing beat and a non swing beat using this value. </para>
        /// <para> For example, let's assume you have a _shufflePeriod of 0.25.
        /// This tells Beat Saber that, every 0.25 beats, it will alternate between a swing beat and a non swing beat,
        /// and will apply an offset if it lands on a swing beat.
        /// The offset value that will be applied to objects on a swing beat is approximately equal to _shuffle* _shufflePeriod beats.
        /// Uncommon in the community. </para>
        /// </remarks>
        [SerializeField] private float _shufflePeriod;
        /// <summary> How long (in seconds) into beatmap audio the level preview will start </summary>
        [SerializeField] private double _previewStartTime;
        /// <summary> Duration (in seconds) of level audio preview </summary>
        [SerializeField] private float _previewDuration;
        /// <summary> Song Filename </summary>
        /// <remarks>
        /// This is the local location to your map's cover image.
        /// Both .jpg and .png are supported image types.
        /// Similar to _songFilename,
        /// this is most often just the name and extension for the cover image (For example, cover.jpg)
        /// </remarks>
        [SerializeField] private string _songFilename;
        /// <summary> Cover Image Filename </summary>
        [SerializeField] private string _coverImageFilename;
        /// <summary> Environment Name </summary>
        /// <remarks>
        /// <list type="number"> DefaultEnvironment
        /// <item> Origins </item>
        /// <item> TriangleEnvironment </item>
        /// <item> BigMirrorEnvironment </item>
        /// <item> NiceEnvironment </item>
        /// <item> KDAEnvironment </item>
        /// <item> MonstercatEnvironment </item>
        /// <item> DragonsEnvironment </item>
        /// <item> CrabRaveEnvironment </item>
        /// <item> PanicEnvironment </item>
        /// <item> RocketEnvironment </item>
        /// <item> GreenDayEnvironment </item>
        /// <item> GreenDayGrenadeEnvironment </item>
        /// <item> TimbalandEnvironment </item>
        /// <item> FitBeatEnvironment </item>
        /// <item> LinkinParkEnvironment </item>
        /// <item> BTSEnvironment </item>
        /// </list>
        /// </remarks>
        [SerializeField] private string _environmentName;
        /// <summary> All Directions Environment Name </summary>
        /// <remarks>
        /// <list type="number"> GlassDesertEnvironment
        /// </list>
        /// </remarks>
        [SerializeField] private string _allDirectionsEnvironmentName;
        /// <summary> Offset between beatmap and audio (seconds). This is Beat Saber's method for tackling off-sync audio </summary>
        [SerializeField] private float _songTimeOffset;
        /// <summary> Top-level custom data </summary>
        /// <remarks>
        /// This is an optional field that contains data unrelated to the official Beat Saber level format.
        /// </remarks>
        [SerializeField] private CustomInfoData _customData;
        /// <summary> Array of Beatmap Sets </summary>
        [SerializeField] private DifficultyBeatmapSet[] _difficultyBeatmapSets;


        /// <inheritdoc cref="_version" />
        public string Version { get => _version; set => _version = value; }

        /// <inheritdoc cref="_songName" />
        public string SongName { get => _songName; set => _songName = value; }

        /// <inheritdoc cref="_songSubName" />
        public string SongSubName { get => _songSubName; set => _songSubName = value; }

        /// <inheritdoc cref="_songAuthorName" />
        public string SongAuthorName { get => _songAuthorName; set => _songAuthorName = value; }

        /// <inheritdoc cref="_levelAuthorName" />
        public string LevelAuthorName { get => _levelAuthorName; set => _levelAuthorName = value; }

        /// <inheritdoc cref="_beatsPerMinute" />
        public float BeatsPerMinute { get => _beatsPerMinute; set => _beatsPerMinute = value; }

        /// <inheritdoc cref="_shuffle" />
        public string Shuffle { get => _shuffle; set => _shuffle = value; }

        /// <inheritdoc cref="_shufflePeriod" />
        public float ShufflePeriod { get => _shufflePeriod; set => _shufflePeriod = value; }

        /// <inheritdoc cref="_previewStartTime" />
        public double PreviewStartTime { get => _previewStartTime; set => _previewStartTime = value; }

        /// <inheritdoc cref="_previewDuration" />
        public float PreviewDuration { get => _previewDuration; set => _previewDuration = value; }

        /// <inheritdoc cref="_songFilename" />
        public string SongFilename { get => _songFilename; set => _songFilename = value; }

        /// <inheritdoc cref="_coverImageFilename" />
        public string CoverImageFilename { get => _coverImageFilename; set => _coverImageFilename = value; }

        /// <inheritdoc cref="_environmentName" />
        public string EnvironmentName { get => _environmentName; set => _environmentName = value; }

        /// <inheritdoc cref="_allDirectionsEnvironmentName" />
        public string AllDirectionsEnvironmentName { get => _allDirectionsEnvironmentName; set => _allDirectionsEnvironmentName = value; }

        /// <inheritdoc cref="_songTimeOffset" />
        public float SongTimeOffset { get => _songTimeOffset; set => _songTimeOffset = value; }

        /// <inheritdoc cref="_customData" />
        public CustomInfoData CustomData { get => _customData; set => _customData = value; }

        /// <inheritdoc cref="_difficultyBeatmapSets" />
        public DifficultyBeatmapSet[] DifficultyBeatmapSets { get => _difficultyBeatmapSets; set => _difficultyBeatmapSets = value; }
    }
}