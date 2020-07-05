// Created by MrJohnWeez
// June 2020

using BeatSaber.beatMapData.obstacles.BeatSaber.BeatMapData;
using BeatSaber.packInfo.difficultyBeatmapSets.difficultyBeatMaps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeatSaber
{
	public struct BeatMapSong
	{
		public BeatMapData beatMapData;
		public _difficultyBeatmaps difficultyBeatmaps;

		public BeatMapSong(BeatMapData beatMapData, _difficultyBeatmaps difficultyBeatmaps)
		{
			this.beatMapData = beatMapData;
			this.difficultyBeatmaps = difficultyBeatmaps;
		}
	}
}