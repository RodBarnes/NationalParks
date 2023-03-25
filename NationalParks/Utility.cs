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

    public static async Task SupportMessage(string subject, bool includeInfo, bool includeLogs)
    {
        var recipients = new List<string>
        {
            Config.SupportEmailAddress
        };

        if (includeInfo)
        {
            var devInfo = $"Mfg: {DeviceInfo.Current.Manufacturer}\n" +
                $"Model: {DeviceInfo.Current.Model}\n" +
                $"Platform: {DeviceInfo.Platform}\n" +
                $"OS Version: {DeviceInfo.Current.Version}\n" +
                $"Name: {DeviceInfo.Current.Name}\n";

            var appInfo = $"Name: {AppInfo.Current.Name}\n" +
                $"Version: {AppInfo.Current.VersionString}\n" +
                $"Build: {AppInfo.Current.BuildString}\n";

        }

        var message = new EmailMessage
        {
            Subject = subject,
            BodyFormat = EmailBodyFormat.PlainText,
            To = new List<string>(recipients)
        };

        if (includeLogs)
        {
            var attachments = new List<EmailAttachment>();
            var files = Directory.GetFiles(Logger.LogPath, $"{Logger.LogName}*");
            foreach (var file in files)
            {
                var attachment = new EmailAttachment(file, "text/plain");
                attachments.Add(attachment);
            }
            message.Attachments = attachments;
        }

        await Email.Default.ComposeAsync(message);
    }
}
