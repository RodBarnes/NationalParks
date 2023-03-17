using CommunityToolkit.Maui.Alerts;

namespace NationalParks;

public static class Logger
{
    public static int LogsToKeep { get; set; } = 2;
    public static string LogPath { get; set; } = FileSystem.Current.AppDataDirectory;
    public static string LogName { get; set; } = AppInfo.Name.Replace(' ', '_');

    private static string Timestamp { get => DateTime.UtcNow.ToString("yyyyMMdd'T'HHmmss'Z'"); }
    private static string Filename { get; set; } = $"{Path.Combine(LogPath, LogName)}_{Timestamp}_log";
    
    public static async Task WriteLogEntry(string entry, string path = "")
    {
        if (String.IsNullOrEmpty(path))
            path = Filename;

        using StreamWriter streamWriter = new(path, true);
        await streamWriter.WriteAsync($"{entry}\n\n");

        await Toast.Make("Exception written to log.").Show();
    }

    public static async Task<string> ReadLog(string path = "")
    {
        string content = "";

        if (String.IsNullOrEmpty(path))
            path = Filename;

        if (File.Exists(path))
        {
            using StreamReader reader = new StreamReader(path);
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
        var files = Directory.GetFiles(FileSystem.Current.AppDataDirectory, $"{AppInfo.Name.Replace(' ', '_')}*");
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

    public static void DeleteLog(string path = "")
    {
        if (String.IsNullOrEmpty(path))
            path = Filename;

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
