namespace NationalParks.ViewModels;

public partial class EventListVM : ListVM
{
    public EventListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Events";
        Term = ResultEvents.Term;
        FilterName = "Event";
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

        ResultEvents result = await GetItems<ResultEvents>(ResultEvents.Term);
        foreach (Event item in result.Data)
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
