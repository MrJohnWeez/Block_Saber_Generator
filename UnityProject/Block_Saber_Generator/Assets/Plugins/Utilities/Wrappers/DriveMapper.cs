using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

#if UNITY_STANDALONE_WIN
public static class DriveMapper
{
    public static char SafeMapDrive(string path)
    {
        char[] driveLetters = "ZYXWVUTSRQPONMLKJIHGFEDCBA".ToCharArray();
        foreach (var letter in driveLetters)
        {
            if (string.IsNullOrEmpty(GetDriveMapping(letter)))
            {
                MapDrive(letter, path);
                return letter;
            }
        }
        throw new Win32Exception("No valid drive mappings found");
    }

    public static void UnmapDrive(char letter)
    {
        if (!DefineDosDevice(2, DevName(letter), null))
        {
            throw new Win32Exception();
        }
    }
    public static string GetDriveMapping(char letter)
    {
        var sb = new StringBuilder(259);
        if (QueryDosDevice(DevName(letter), sb, sb.Capacity) == 0)
        {
            // Return empty string if the drive is not mapped
            int err = Marshal.GetLastWin32Error();
            if (err == 2)
            {
                return "";
            }

            throw new Win32Exception();
        }
        return sb.ToString()[4..];
    }


    private static void MapDrive(char letter, string path)
    {
        if (!DefineDosDevice(0, DevName(letter), path))
        {
            throw new Win32Exception();
        }
    }

    private static string DevName(char letter)
    {
        return new string(char.ToUpper(letter), 1) + ":";
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool DefineDosDevice(int flags, string devname, string path);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int QueryDosDevice(string devname, StringBuilder buffer, int bufSize);
}
#endif