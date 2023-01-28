namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class CampgroundListVM : ListVM
{
    public CampgroundListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Campgrounds";
        Term = ResultCampgrounds.Term;
        FilterName = "Campground";
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
