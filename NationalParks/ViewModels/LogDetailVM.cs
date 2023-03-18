using System.Reflection;
using System.Text;

namespace NationalParks.ViewModels;

public partial class LogDetailVM : BaseVM
{
    [ObservableProperty] CollapsibleListVM log1;
    [ObservableProperty] CollapsibleListVM log2;
    [ObservableProperty] CollapsibleListVM log3;
    [ObservableProperty] bool noData;
    [ObservableProperty] bool hasData;

    IGetDeviceInfo getDeviceInfo;

    readonly List<object>[] lists = new List<object>[3];

    public LogDetailVM(IGetDeviceInfo getDeviceInfo)
    {
        Title = "Logs";
        this.getDeviceInfo = getDeviceInfo;
    }

    [RelayCommand]
    public async Task PopulateData()
    {
        try
        {
            var files = Directory.GetFiles(Logger.LogPath, $"{Logger.LogName}*");
            var nbr = files.Length;
            for (int i = 0; i < nbr; i++)
            {
                var content = await Logger.ReadLog(files[i]);
                var array = content.Split('\n');
                var list = array.ToList();

                lists[i] = list.ToList<object>();
            }

            if (files.Length > 0)
            {
                Log1 = new CollapsibleListVM($"{Path.GetFileName(files[0])}", false, lists[0]);
            }
            else
            {
                Log1 = new CollapsibleListVM("", false, new List<object>());
            }

            if (files.Length > 1)
            {
                Log2 = new CollapsibleListVM($"{Path.GetFileName(files[1])}", false, lists[1]);
            }
            else
            {
                Log2 = new CollapsibleListVM("", false, new List<object>());
            }

            if (files.Length > 2)
            {
                Log3 = new CollapsibleListVM($"{Path.GetFileName(files[2])}", false, lists[2]);
            }
            else
            {
                Log3 = new CollapsibleListVM("", false, new List<object>());
            }

            SetVisibleElements(Log1.Items.Count > 0);
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            var codeInfo = new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType);
            await Logger.WriteLogEntry($"{codeInfo.ObjectName}.{codeInfo.MethodName}: {msg}");
        }
    }

    [RelayCommand]
    public async Task DeleteLogs()
    {
        try
        {
            var files = Directory.GetFiles(Logger.LogPath, $"{Logger.LogName}*");
            foreach(var file in files)
            {
                Logger.DeleteLog(file);
            }

            await PopulateData();
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            var codeInfo = new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType);
            await Logger.WriteLogEntry($"{codeInfo.ObjectName}.{codeInfo.MethodName}: {msg}");
        }
    }

    [RelayCommand]
    public async Task SendLogs()
    {
        try
        {
            StringBuilder logInfo = new();

            var files = Directory.GetFiles(Logger.LogPath, $"{Logger.LogName}*");
            foreach(var file in files)
            {
                logInfo.Append($"Logname: {Path.GetFileName(file)}\n");
                var content = await Logger.ReadLog(file);
                logInfo.Append($"Content: {content}");
            }

            var devInfo = $"Mfg: {DeviceInfo.Current.Manufacturer}\n" +
                $"Model: {DeviceInfo.Current.Model}\n" +
                $"Platform: {DeviceInfo.Platform}\n" +
                $"OS Version: {DeviceInfo.Current.Version}\n" +
                $"Name: {DeviceInfo.Current.Name}\n" +
                $"ID: {getDeviceInfo.GetDeviceID()}\n";

            var appInfo = $"Name: {AppInfo.Current.Name}\n" +
                $"Version: {AppInfo.Current.VersionString}\n" +
                $"Build: {AppInfo.Current.BuildString}\n";

            var subject = "NPS Info Logs";
            var body = $"App Info\n{appInfo}\nDevice Info\n{devInfo}\n{logInfo}";
            var recipients = new List<string>
            {
                Config.SupportEmailAddress
            };

            var message = new EmailMessage
            {
                Subject = subject,
                Body = body,
                BodyFormat = EmailBodyFormat.PlainText,
                To = new List<string>(recipients)
            };

            await Email.Default.ComposeAsync(message);
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            var codeInfo = new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType);
            await Logger.WriteLogEntry($"{codeInfo.ObjectName}.{codeInfo.MethodName}: {msg}");
        }
    }

    private void SetVisibleElements(bool value)
    {
        HasData = value;
        NoData = !value;
    }
}
