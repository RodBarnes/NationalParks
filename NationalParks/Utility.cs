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

        if (includeLogs)
        {
            var attachments = new List<EmailAttachment>();
            var files = Directory.GetFiles(Logger.LogPath, $"{Logger.LogName}*");
            foreach (var file in files)
            {
                var attachment = new EmailAttachment(file, "text/plain");
                attachments.Add(attachment);
            }
            if (attachments.Count > 0)
            {
                message.Attachments = attachments;
            }
        }

        await Email.Default.ComposeAsync(message);
    }
}
