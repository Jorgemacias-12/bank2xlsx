namespace Bank2xlsx.Helpers;

public static class FileSizeFormatter
{
    private const int Scale = 1024;

    private const string BytesSuffix = "B";
    private const string KilobytesSuffix = "KB";
    private const string MegabytesSuffix = "MB";
    private const string GigabytesSuffix = "GB";
    private const string TerabytesSuffix = "TB";

    private static readonly string[] SizeSuffixes =
    [
        BytesSuffix,
        KilobytesSuffix,
        MegabytesSuffix,
        GigabytesSuffix,
        TerabytesSuffix
    ];

    public static string Format(long bytes)
    {
        double size = bytes;
        int suffixIndex = 0;

        while (size >= Scale &&
               suffixIndex < SizeSuffixes.Length - 1)
        {
            suffixIndex++;
            size /= Scale;
        }

        return $"{size:0.##} {SizeSuffixes[suffixIndex]}";
    }
}