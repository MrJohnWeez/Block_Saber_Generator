using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BeatMapData
{
	public string _version;
	public BeatSaber.SongData._customData _customData;
	public _events[] _events;
	public _notes[] _notes;
	public _obstacles[] _obstacles;
}