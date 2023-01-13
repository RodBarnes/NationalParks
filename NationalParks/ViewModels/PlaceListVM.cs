using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class PlaceListVM : ListVM
{
    readonly IConnectivity connectivity;

    readonly string baseTitle = "Places";

    public ObservableCollection<Models.Place> Places { get; } = new();

    public PlaceListVM(IConnectivity connectivity, IGeolocation geolocation) : base(geolocation)
    {
        IsBusy = false;
        base.BaseTitle = "Places";
        this.connectivity = connectivity;
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    public void ClearData()
    {
        Places.Clear();
        IsPopulated = false;
    }

    [RelayCommand]
    async Task GetItems()
    {
        if (IsBusy)
            return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            IsBusy = true;

            GetFilterSelections();

            // Populate the list
            ResultPlaces result = await GetPlacesData(StartItems, LimitItems, StatesFilter);

            StartItems += result.Data.Count;
            TotalItems = result.Total;
            IsPopulated = true;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task<ResultPlaces> GetPlacesData(int startItems, int limitItems, string statesFilter)
    {
        Park park;
        ResultPlaces result = await DataService.GetPlacesAsync(startItems, limitItems, statesFilter);
        foreach (var place in result.Data)
        {
            // This code addresses the condition where there is no location of the place
            // but it has at least one related park
            if (place.DLatitude < 0 && place.RelatedParks.Count > 0)
            {
                ResultParks resultPark = await DataService.GetParkForParkCodeAsync(place.RelatedParks[0].ParkCode);
                if (resultPark.Data.Count == 1)
                {
                    park = resultPark.Data[0];
                    place.Latitude = park.Latitude;
                    place.Longitude = park.Longitude;
                }
            }
            Places.Add(place);
        }

        return result;
    }
}
