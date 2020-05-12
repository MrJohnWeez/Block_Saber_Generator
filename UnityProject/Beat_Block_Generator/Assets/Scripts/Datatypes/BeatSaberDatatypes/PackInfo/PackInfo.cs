using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PackInfo
{
	public string _version;
	public string _songName;
	public string _songSubName;
	public string _songAuthorName;
	public string _levelAuthorName;
	public float _beatsPerMinute;
	public string _shuffle;
	public float _shufflePeriod;
	public double _previewStartTime;
	public float _previewDuration;
	public string _songFilename;
	public string _coverImageFilename;
	public string _environmentName;
	public float _songTimeOffset;
	public BeatSaber.Contributors._customData _customData;
	public _difficultyBeatmapSets[] _difficultyBeatmapSets;
}