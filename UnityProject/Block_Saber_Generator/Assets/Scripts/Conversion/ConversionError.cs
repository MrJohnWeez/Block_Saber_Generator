
namespace MJW.Conversion
{
    public enum ConversionError
    {
        None,
        InvalidZipFile,
        InvalidBeatMap,
        MissingInfo,
        MissingSongData,
        MissingSongFile,
        FailedToCopyFile,
        NoMapData,
        UnzipError,
        OtherFail,
        Canceled
    }
}
