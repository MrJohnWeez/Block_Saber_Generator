using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BeatSaber.packInfo.difficultyBeatmapSets.difficultyBeatMaps
{
	[Serializable]
	public class _difficultyBeatmaps
	{
		public string _difficulty;
		public int _difficultyRank;
		public string _beatmapFilename;
		public float _noteJumpMovementSpeed;
		public float _noteJumpStartBeatOffset;
		public _customData _customData;
	}
}
