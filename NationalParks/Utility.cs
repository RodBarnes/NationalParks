using CommunityToolkit.Maui.Alerts;
using System.Text;

namespace NationalParks;

public static class Utility
{
    public static string ParseException(Exception ex)
    {
        var sb = new StringBuilder();
        sb.Append($"{ex.Source}--{ex.Message}");

        var iex = ex.InnerException;
        while (iex != null)
        {
            sb.Append($"\n{iex.Source}--{iex.Message}");
            iex = iex.InnerException;
        }

        return sb.ToString();
    }

    public static async Task HandleException(Exception ex, CodeInfo codeInfo)
    {
        var msg = Utility.ParseException(ex);
        await Logger.WriteLogEntry($"{codeInfo.ObjectName}.{codeInfo.MethodName}: {msg}");

        await Toast.Make("Exception written to log.").Show();
    }

    public static async Task SupportMessage(string subject, bool includeLogs)
    {
        var filename = "log.txt";
        var filepath = Path.Combine(Logger.LogPath, filename);

        var recipients = new List<string>
        {
            Config.SupportEmailAddress
        };

        var message = new EmailMessage
        {
            Subject = subject,
            BodyFormat = EmailBodyFormat.PlainText,
            To = new List<string>(recipients)
        };

        var info =
            $"Device Mfg: {DeviceInfo.Current.Manufacturer}\n" +
            $"Device Model: {DeviceInfo.Current.Model}\n" +
            $"Device Platform: {DeviceInfo.Platform}\n" +
            $"Device OS Version: {DeviceInfo.Current.Version}\n" +
            $"Device Name: {DeviceInfo.Current.Name}\n" +
            $"App Name: {AppInfo.Current.Name}\n" +
            $"App Version: {AppInfo.Current.VersionString}\n" +
            $"App Build: {AppInfo.Current.BuildString}\n\n";

        var attachments = new List<EmailAttachment>();

        var files = Directory.GetFiles(Logger.LogPath, $"{Logger.LogName}*");
        if (includeLogs && files.Length > 0)
        {
            if (File.Exists(filepath))
            {
                // Delete any existing send file
                Logger.DeleteLog(filename);
            }

            await Logger.WriteLogEntry(info, filename);

            foreach (var file in files)
            {
                await Logger.WriteLogEntry(Path.GetFileName(file), filename);
                var log = await Logger.ReadLog(file);
                await Logger.WriteLogEntry($"{log}\n\n", filename);
            }

            var attachment = new EmailAttachment(filepath, "text/plain");
            attachments.Add(attachment);

            if (attachments.Count > 0)
            {
                message.Attachments = attachments;
            }
        }

        await Email.Default.ComposeAsync(message);
    }
}
