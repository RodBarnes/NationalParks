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

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    new async Task GetItems()
    {
        // Populate the list
        Result result = await base.GetItems();
        if (result != null)
        {
            ResultCampgrounds resultCampgrounds = (ResultCampgrounds)result;
            foreach (var item in resultCampgrounds.Data)
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
            while (TotalItems > Items.Count)
            {
                ProgressClosest = (double)Items.Count / (double)TotalItems;
                await GetItems();
            }
            LimitItems = 20;
            IsFindingClosest = false;
        }

        await base.GetClosest();
    }
}
