using BeatSaber.beatMapData.customData;
using BeatSaber.beatMapData.events;
using BeatSaber.beatMapData.notes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeatSaber.beatMapData.obstacles
{
	namespace BeatSaber.BeatMapData
	{
		[Serializable]
		public class BeatMapData
		{
			public string _version;
			public _customData _customData;
			public _events[] _events;
			public _notes[] _notes;
			public _obstacles[] _obstacles;
		}
	}
}