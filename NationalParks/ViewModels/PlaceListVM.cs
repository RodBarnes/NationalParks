using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class PlaceListVM : ListVM
{
    public PlaceListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Places";
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    async Task GetItems()
    {
        Result result = await GetItems(ResultPlaces.Term);
        if (result != null)
        {
            ResultPlaces resultPlaces = (ResultPlaces)result;
            foreach (var item in resultPlaces.Data)
                Items.Add(item);
            StartItems += resultPlaces.Data.Count;
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

        await GetClosest(ResultPlaces.Term);
    }
}
