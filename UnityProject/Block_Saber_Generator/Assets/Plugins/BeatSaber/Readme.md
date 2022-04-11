# Beat Saber C# Json
Use these Json files to parse beat saber map data into applications.

Quick repo I threw together for Beat Saber map data parsing

## Usage

Use any json library to load Info.dat into a BeatSaberMap object

**Ignore the MapLoader.cs** script as it is dependent on another library

Quick Example Usage below:
- **loadedMapData** is all beat saber map data loaded
```C#
string infoPath = Path.Combine(tempUnZipPath, "info.dat");
using (var sr = new StreamReader(mapPath))
{
    string infoFileData;
    infoFileData = sr.ReadToEnd();
    Info info = JsonUtility.FromJson<Info>(infoFileData);

    List<MapData> mapDatas = new List<MapData>();
    foreach (var bms in info.DifficultyBeatmapSets)
    {
        foreach (var bm in bms.DifficultyBeatmaps)
        {
            string mapPath = Path.Combine(tempUnZipPath, bm.BeatmapFilename);
            using (var sr = new StreamReader(mapPath))
            {
                var fileData = sr.ReadToEnd();
                MapData mapData = JsonUtility.FromJson<MapData>(fileData);
                mapDatas.Add(mapData);
            }
        }
    }
    var loadedMapData = new BeatSaberMap(info, mapDatas.ToArray(), tempUnZipPath);
}
```
These files can be used for a wide variety of applications and programs.

Some example include: Unity applications, DotNet, C#, map mods, modding, mapping engines, maps, beat saber, BTS character editors, editors,

# Future Improvements
- Update to latest beat saber version (with chain notes)
- Cleanup XML

# Credits
John Wiesner - creator of json files and extra data

Most xml comment information - https://bsmg.wiki/mapping/map-format.html#schemas