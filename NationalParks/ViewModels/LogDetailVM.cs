namespace NationalParks.ViewModels;

public partial class LogDetailVM : BaseVM
{
    [ObservableProperty] CollapsibleListVM log1;
    [ObservableProperty] CollapsibleListVM log2;
    [ObservableProperty] CollapsibleListVM log3;
    [ObservableProperty] bool noLogs;

    readonly List<object>[] lists = new List<object>[3];

    public LogDetailVM()
    {
        Title = "Logs";
    }

    [RelayCommand]
    public async Task PopulateData()
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
            NoLogs = false;
        }
        else
        {
            Log1 = new CollapsibleListVM("", false, new List<object>());
            NoLogs = true;
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
    }

    [RelayCommand]
    public void DeleteLogs()
    {
        var files = Directory.GetFiles(Logger.LogPath, $"{Logger.LogName}*");
        var nbr = files.Length;
        for (int i = 0; i < nbr; i++)
        {
            Logger.DeleteLog(files[i]);
        }

        PopulateData();
    }

    [RelayCommand]
    public void EmailLogs()
    {
        Shell.Current.DisplayAlert("Email", "On their way...", "OK");
    }
}
