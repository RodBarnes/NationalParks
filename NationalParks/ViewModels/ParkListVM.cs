namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ParkListVM : ListVM
{
    public ParkListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Parks";
        Term = ResultParks.Term;
        FilterName = "Park";
    }

    [RelayCommand]
    new async Task GetItems()
    {
        // Populate the list
        Result result = await base.GetItems();
        if (result != null)
        {
            ResultParks resultParks = (ResultParks)result;
            foreach (var item in resultParks.Data)
                Items.Add(item);
            IsPopulated = true;
        }
    }

    [RelayCommand]
    new async Task GetClosest()
    {
        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            LimitItems = 50;
            IsFindingClosest = true;
            while (TotalItems > Items.Count && IsFindingClosest)
            {
                ProgressClosest = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            LimitItems = 20;
        }

        if (IsFindingClosest)
        {
            await base.GetClosest();
        }
    }
}
