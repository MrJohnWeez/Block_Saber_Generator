using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Minecraft;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MJW.Conversion
{
    public class ConversionObject : MonoBehaviour
    {
        [Header("Progress Bar Colors")]
        [SerializeField] private Color _finishSuccess;
        [SerializeField] private Color _finishFail;

        [Header("Connected Objects")]
        [SerializeField] private TMP_Text title = null;
        [SerializeField] private TMP_Text _status = null;
        [SerializeField] private Slider _progressBar = null;
        [SerializeField] private Image _progressFill = null;
        [SerializeField] private Image _trash = null;

        [Header("Toggle Images")]
        [SerializeField] private Sprite _trashOpen = null;
        [SerializeField] private Sprite _trashClosed = null;


        private string inputPath = "";
        private string outputPath = "";
        private string fileName = "";
        private CancellationTokenSource _asyncSourceCancel = null;


        public string InputPath { get => inputPath; set => inputPath = value; }
        public string OutputPath { get => outputPath; set => outputPath = value; }
        public string FileName { get => fileName; set => fileName = value; }


        public event Action<ConversionObject> ObjectFinished;
        public event Action<ConversionObject> ObjectDeleted;


        public void Setup(string zipFilePathToConvert, string outputPathOfTwoMinecraftFiles)
        {
            InputPath = zipFilePathToConvert;
            OutputPath = outputPathOfTwoMinecraftFiles;
            FileName = Path.GetFileNameWithoutExtension(zipFilePathToConvert);
            title.text = FileName;
            _status.text = "Waiting...";
            _progressBar.value = 0;
        }

        public async Task ConvertAsync()
        {
            _progressBar.value = _progressBar.maxValue;
            _status.text = "Converting...";
            _asyncSourceCancel = new CancellationTokenSource();
            int uuid = UnityEngine.Random.Range(-99999999, 99999999);
            ConversionError error = await ConvertZip.ConvertAsync(InputPath, OutputPath, uuid, _asyncSourceCancel.Token);
            if (error != ConversionError.None)
            {
                _progressBar.value = _progressBar.maxValue;
                _progressFill.color = _finishFail;
                switch (error)
                {
                    case ConversionError.None:
                        break;
                    case ConversionError.MissingInfo:
                        _status.text = "Failed: missing info.dat";
                        break;
                    case ConversionError.MissingSongData:
                        _status.text = "Failed: missing song data";
                        break;
                    case ConversionError.MissingSongFile:
                        _status.text = "Failed: missing song file";
                        break;
                    case ConversionError.OtherFail:
                        _status.text = "Failed!";
                        break;
                    case ConversionError.InvalidZipFile:
                        _status.text = "Failed: invalid zip file";
                        break;
                    case ConversionError.InvalidBeatMap:
                        _status.text = "Failed: invalid beat map";
                        break;
                    case ConversionError.FailedToCopyFile:
                        _status.text = "Failed: unable to copy files";
                        break;
                    case ConversionError.NoMapData:
                        _status.text = "Failed: no map data";
                        break;
                    case ConversionError.UnzipError:
                        _status.text = "Failed: unzip error";
                        break;
                    default:
                        _status.text = "Failed: unknown error!";
                        break;
                }
            }
            else
            {
                _progressBar.value = _progressBar.maxValue;
                _progressFill.color = _finishSuccess;
                _status.text = "Done!";
            }
            _asyncSourceCancel.Dispose();
            _asyncSourceCancel = null;
            ObjectFinished?.Invoke(this);
        }

        public void DeleteSelf()
        {
            if (_asyncSourceCancel != null)
            {
                _asyncSourceCancel?.Cancel();
            }
            ObjectDeleted?.Invoke(this);
            Destroy(gameObject);
        }

        public void TrashOpen()
        {
            _trash.sprite = _trashOpen;
        }

        public void TrashClosed()
        {
            _trash.sprite = _trashClosed;
        }
    }
}
