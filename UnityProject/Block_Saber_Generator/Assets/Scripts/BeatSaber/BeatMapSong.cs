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
		public string beatmapCharacteristicName;

		public BeatMapSong(BeatMapData beatMapData, _difficultyBeatmaps difficultyBeatmaps, string beatmapCharacteristicName = "")
		{
			this.beatMapData = beatMapData;
			this.difficultyBeatmaps = difficultyBeatmaps;
			this.beatmapCharacteristicName = beatmapCharacteristicName;
		}
	}
}