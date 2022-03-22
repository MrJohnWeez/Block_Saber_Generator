using System.Threading;
using UnityEngine;
using BeatSaber;

public class Test : MonoBehaviour
{
    private BeatSaberMap beatData;


    protected void Start()
    {
        var temporaryPath = Application.temporaryCachePath;
        var _asyncSourceCancel = new CancellationTokenSource();
        string zipPath = @"C:\Users\John\Desktop\TestPacks\TestPackSong.zip";
        beatData = MapLoader.GetDataFromMapZip(zipPath, temporaryPath, _asyncSourceCancel.Token).Result;
        Debug.Log(beatData.Info.Version);
        beatData.DeleteFolder();
    }
}
