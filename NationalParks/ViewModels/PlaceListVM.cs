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
        Term = ResultPlaces.Term;
        await GetItems();
    }

    [RelayCommand]
    new async Task GetItems()
    {
        Result result = await base.GetItems();
        if (result != null)
        {
            ResultPlaces resultPlaces = (ResultPlaces)result;
            foreach (var item in resultPlaces.Data)
            {
                // This code addresses the condition where Place has no location but
                // but it has at least one related park
                if (item.DLatitude < 0 && item.RelatedParks.Count > 0)
                {
                    ResultParks resultPark = await DataService.GetParkForParkCodeAsync(item.RelatedParks[0].ParkCode);
                    if (resultPark.Data.Count == 1)
                    {
                        var park = resultPark.Data[0];
                        item.Latitude = park.Latitude;
                        item.Longitude = park.Longitude;
                    }
                }
                Items.Add(item);
            }
            IsPopulated = true;
        }
    }

    [RelayCommand]
    new async Task GetClosest()
    {
        if (Items.Count < TotalItems)
        {
            // Get the rest of the items
            LimitItems = 150;
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
