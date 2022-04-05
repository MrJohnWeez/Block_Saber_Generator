using BeatSaber.Data;

namespace BeatSaber
{
    public class MapDataInfo
    {
        private DifficultyBeatmap difficultyBeatmapInfo = null;
        private MapData mapData = null;


        public DifficultyBeatmap DifficultyBeatmapInfo { get => difficultyBeatmapInfo; set => difficultyBeatmapInfo = value; }

        public MapData MapData { get => mapData; set => mapData = value; }


        public MapDataInfo(DifficultyBeatmap difficultyBeatmap, MapData mapData)
        {
            this.difficultyBeatmapInfo = difficultyBeatmap;
            this.mapData = mapData;
        }
    }
}
