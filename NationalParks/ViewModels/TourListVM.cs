using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class TourListVM : ListVM
{
    public TourListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Tours";
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    async Task GetItems()
    {
        Result result = await GetItems(ResultTours.Term);
        if (result != null)
        {
            ResultTours resultTours = (ResultTours)result;
            foreach (var item in resultTours.Data)
                Items.Add(item);
            StartItems += resultTours.Data.Count;
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

        await GetClosest(ResultTours.Term);
    }
}
