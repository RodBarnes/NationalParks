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
    public new async Task PopulateData()
    {
        await GetItems();
        await base.PopulateData();
    }

    [RelayCommand]
    public async Task GetItems()
    {
        if (IsBusy)
            return;

        ResultVideos result = await GetItems<ResultVideos>(ResultVideos.Term);
        foreach (Multimedia item in result.Data)
        {
            item.FillMainImage();
            Items.Add(item);
        }
        TotalItems = result.Total;
        IsPopulated = true;
    }

    [RelayCommand]
    public new async Task GetClosest()
    {
        if (IsBusy)
            return;

        Progress.IsVisible = true;

        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            LimitItems = 50;
            while (TotalItems > Items.Count && Progress.IsVisible)
            {
                Progress.Position = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            LimitItems = 20;
        }

        if (Progress.IsVisible)
        {
            await base.GetClosest();
            Progress.IsVisible = false;
        }

        Progress.IsVisible = false;
    }
}
