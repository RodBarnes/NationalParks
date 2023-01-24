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
        Term = ResultTours.Term;
        await GetItems();
    }

    [RelayCommand]
    new async Task GetItems()
    {
        Result result = await base.GetItems();
        if (result != null)
        {
            ResultTours resultTours = (ResultTours)result;
            foreach (var item in resultTours.Data)
            {
                // This addresses the condition that Tours don't have a location but the associated park does
                ResultParks resultPark = await DataService.GetParkForParkCodeAsync(item.Park.ParkCode);
                if (resultPark.Data.Count == 1)
                {
                    var park = resultPark.Data[0];
                    item.Latitude = park.Latitude;
                    item.Longitude = park.Longitude;
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
