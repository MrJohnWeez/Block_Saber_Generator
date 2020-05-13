﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BeatMapSong
{
	BeatMapData beatMapData;
	_difficultyBeatmaps difficultyBeatmaps;

	public BeatMapSong(BeatMapData beatMapData, _difficultyBeatmaps difficultyBeatmaps)
	{
		this.beatMapData = beatMapData;
		this.difficultyBeatmaps = difficultyBeatmaps;
	}
}
