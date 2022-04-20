using System;
using System.Collections.Generic;
using BeatSaber.Data;
using Utilities.Wrappers;

namespace BeatSaber
{
    public class BeatSaberMap
    {
        private Info infoData;
        private Dictionary<string, MapDataInfo> mapDataInfos;
        private string extractedFilePath;
        private Guid guidId;


        public Info InfoData { get => infoData; set => infoData = value; }

        public Dictionary<string, MapDataInfo> MapDataInfos { get => mapDataInfos; set => mapDataInfos = value; }

        public string ExtractedFilePath { get => extractedFilePath; set => extractedFilePath = value; }

        public Guid GuidId { get => guidId; set => guidId = value; }


        public BeatSaberMap(Info info, Dictionary<string, MapDataInfo> mapDataInfos, string filePath)
        {
            infoData = info;
            this.mapDataInfos = mapDataInfos;
            extractedFilePath = filePath;
            guidId = Guid.NewGuid();
        }


        public void DeleteFolder()
        {
            SafeFileManagement.DeleteDirectory(extractedFilePath);
        }
    }
}
