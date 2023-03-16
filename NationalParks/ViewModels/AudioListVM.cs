namespace NationalParks.ViewModels;

public partial class AudioListVM : ListVM
{
    public AudioListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Audios";
        Term = ResultAudios.Term;
        FilterName = "Audio";
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

        ResultAudios result = await GetItems<ResultAudios>(ResultAudios.Term);
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
