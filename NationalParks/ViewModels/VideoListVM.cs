using System.Reflection;

namespace NationalParks.ViewModels;

public partial class VideoListVM : ListVM
{
    public VideoListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Videos";
        Term = ResultVideos.Term;
        FilterName = "Video";
        AllowFilterStates = true;
    }

    [RelayCommand]
    public async Task GetItems()
    {
        if (IsBusy)
            return;

        try
        {
            ResultVideos result = await GetItems<ResultVideos>(ResultVideos.Term);
            foreach (Multimedia item in result.Data)
            {
                item.FillMainImage();
                Items.Add(item);
            }
            TotalItems = result.Total;
            IsPopulated = true;
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            var codeInfo = new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType);
            await Logger.WriteLogEntry($"{codeInfo.ObjectName}.{codeInfo.MethodName}: {msg}");
        }
    }

    [RelayCommand]
    public new async Task GetClosest()
    {
        if (IsBusy)
            return;

        ProgressPanel.IsVisible = true;

        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            LimitItems = 50;
            while (TotalItems > Items.Count && ProgressPanel.IsVisible)
            {
                ProgressPanel.Position = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            LimitItems = 20;
        }

        if (ProgressPanel.IsVisible)
        {
            await base.GetClosest();
            ProgressPanel.IsVisible = false;
        }

        ProgressPanel.IsVisible = false;
    }

    public new async Task PopulateData()
    {
        await GetItems();
        await base.PopulateData();
    }
}
