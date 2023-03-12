namespace NationalParks.ViewModels;

public partial class ParkListVM : ListVM
{
    public ParkListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Parks";
        Term = ResultParks.Term;
        FilterName = "Park";
        AllowFilterStates = true;
        AllowFilterActivities = true;
        AllowFilterTopics = true;
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

        ResultParks result = await GetItems<ResultParks>(ResultParks.Term);
        foreach (Park item in result.Data)
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

        ShowClosestProgress = true;

        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            LimitItems = 50;
            while (TotalItems > Items.Count && ShowClosestProgress)
            {
                ProgressClosest = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            LimitItems = 20;
        }

        if (ShowClosestProgress)
        {
            await base.GetClosest();
            ShowClosestProgress = false;
        }

        IsBusy = false;
    }
}
