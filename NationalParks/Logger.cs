namespace NationalParks;

public static class Logger
{
    public static int LogsToKeep { get; set; } = 2;
    public static string LogPath { get; set; } = FileSystem.Current.CacheDirectory;
    public static string LogName { get; set; } = AppInfo.Name.Replace(' ', '_');

    private static string Timestamp { get => DateTime.UtcNow.ToString("yyyyMMdd'T'HHmmss'Z'"); }
    private static string Filename { get; set; } = $"{Path.Combine(LogPath, LogName)}_{Timestamp}";
    
    public static async Task WriteLogEntry(string entry, string filename = "")
    {
        string path = Filename;

        if (!String.IsNullOrEmpty(filename))
        {
            // Read from a specifid log file
            path = $"{Path.Combine(LogPath, filename)}";
        }

        using StreamWriter streamWriter = new(path, true);
        {
            await streamWriter.WriteAsync($"{entry}\n\n");
        }
    }

    public static async Task<string> ReadLog(string filename = "")
    {
        string content = "";
        string path = Filename;

        if (!String.IsNullOrEmpty(filename))
        {
            // Read from a specifid log file
            path = $"{Path.Combine(LogPath, filename)}";
        }

        if (File.Exists(filename))
        {
            using StreamReader reader = new(path);
            content = await reader.ReadToEndAsync();
        }
        else
        {
            throw new Exception("No log file");
        }

        return content;
    }

    public static void RotateLogs(int nbrToKeep = -1)
    {
        // Delete all but the last 'N' logs
        var files = Directory.GetFiles(LogPath, $"{LogName}*");
        var i = 0;

        if (nbrToKeep == -1)
            nbrToKeep = LogsToKeep;

        foreach(var file in files)
        {
            if (i < nbrToKeep)
            {
                i++;
            }
            else
            {
                File.Delete(file);
            }
        }
    }

    public static void DeleteLog(string filename)
    {
        string path = Filename;

        if (!String.IsNullOrEmpty(filename))
        {
            // Read from a specifid log file
            path = $"{Path.Combine(LogPath, filename)}";
        }

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
