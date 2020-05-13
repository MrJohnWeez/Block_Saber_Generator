using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DatapackGenerator
{
	private readonly string[] _excludeExtensions = { ".meta" };
	private const int C_numberOfIORetryAttempts = 5;

	// Datapack hardcoded names
	private const string C_Data = "data";
	private const string C_Minecraft = "minecraft";
	private const string C_Functions = "functions";
	private const string C_McFunction = ".mcfunction";
	private const string C_Tags = "tags";
	private const string C_FakePlayerChar = "#";
	private const string C_Slash = "/";


	private string pathOfDatapackTemplate = Path.Combine(Application.dataPath, "StreamingAssets", "TemplateDatapack");
	
	private Dictionary<string, string> _keyVars = new Dictionary<string, string>();

	private string _unzippedFolderPath = "";
	private PackInfo _packInfo;
	List<BeatMapSong> _beatMapSongList;

	public DatapackGenerator(string unzippedFolderPath, PackInfo packInfo, List<BeatMapSong> beatMapSongList, string datapackOutputPath)
	{
		this._unzippedFolderPath = unzippedFolderPath;
		this._packInfo = packInfo;
		this._beatMapSongList = beatMapSongList;
		this._datapackOutputPath = datapackOutputPath;
	}

	public void Generate()
	{
		if (Directory.Exists(_unzippedFolderPath) && _packInfo != null && _beatMapSongList.Count > 0)
		{
			SafeFileManagement.DirectoryCopy(pathOfDatapackTemplate, _unzippedFolderPath, true, _excludeExtensions, C_numberOfIORetryAttempts);
		}
	}
}
