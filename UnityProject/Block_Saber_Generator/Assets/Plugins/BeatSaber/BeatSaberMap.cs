using System;
using BeatSaber.Data;

namespace BeatSaber
{
    public class BeatSaberMap
    {
        private Info _info;
        private MapData[] _mapDatas;
        private string _extractedFilePath;
        private Guid _guid;


        public Info Info { get => _info; set => _info = value; }

        public MapData[] MapData { get => _mapDatas; set => _mapDatas = value; }

        public string ExtractedFilePath { get => _extractedFilePath; set => _extractedFilePath = value; }

        public Guid Guid { get => _guid; set => _guid = value; }


        public BeatSaberMap(Info info, MapData[] mapDatas, string filePath)
        {
            _info = info;
            _mapDatas = mapDatas;
            _extractedFilePath = filePath;
            _guid = Guid.NewGuid();
        }

        public void DeleteFolder()
        {
            SafeFileManagement.DeleteDirectory(_extractedFilePath);
        }
    }
}
