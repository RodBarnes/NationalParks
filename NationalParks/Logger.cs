using CommunityToolkit.Maui.Alerts;

namespace NationalParks;

public static class Logger
{
    public static string Filename { get; set; } = $"{Path.Combine(FileSystem.Current.AppDataDirectory, AppInfo.Name.Replace(' ','_'))}_log";
    
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
