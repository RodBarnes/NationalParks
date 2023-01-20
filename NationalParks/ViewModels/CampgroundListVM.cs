using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class CampgroundListVM : ListVM
{
    public CampgroundListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Campgrounds";
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    async Task GetItems()
    {
        // Populate the list
        Result result = await GetItems(ResultCampgrounds.Term);
        if (result != null)
        {
            ResultCampgrounds resultCampgrounds = (ResultCampgrounds)result;
            foreach (var item in resultCampgrounds.Data)
                Items.Add(item);
            StartItems += resultCampgrounds.Data.Count;
            IsPopulated = true;
        }
    }

    [RelayCommand]
    async Task GetClosest()
    {
        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            while (TotalItems > StartItems)
            {
                await GetItems();
            }
        }

        await GetClosest(ResultCampgrounds.Term);
    }
}
