using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class TourListVM : ListVM
{
    readonly IConnectivity connectivity;

    [ObservableProperty] public ObservableCollection<Models.BaseModel> items;

    public TourListVM(IConnectivity connectivity, IGeolocation geolocation) : base(geolocation)
    {
        IsBusy = false;
        BaseTitle = "Tours";
        this.connectivity = connectivity;
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    public void ClearData()
    {
        Items.Clear();
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
            ResultTours result = await DataService.GetToursAsync(StartItems, LimitItems, StatesFilter);

            // This should be done in the DataServices?
            // This addresses that Tours don't have a location but the associated park does
            //ResultParks resultPark = await DataService.GetParkForParkCodeAsync(tour.Park.ParkCode);
            //if (resultPark.Data.Count == 1)
            //{
            //    park = resultPark.Data[0];
            //    tour.Latitude = park.Latitude;
            //    tour.Longitude = park.Longitude;
            //}

            Items = new(result.Data);
            StartItems += result.Data.Count;
            TotalItems = result.Total;
            IsPopulated = true;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task<ResultTours> GetToursData(int startItems, int limitItems, string statesFilter)
    {
        Park park;
        ResultTours result = await DataService.GetToursAsync(startItems, limitItems, statesFilter);
        foreach (var tour in result.Data)
        {
            ResultParks resultPark = await DataService.GetParkForParkCodeAsync(tour.Park.ParkCode);
            if (resultPark.Data.Count == 1)
            {
                park = resultPark.Data[0];
                tour.Latitude = park.Latitude;
                tour.Longitude = park.Longitude;
            }
            Items.Add(tour);
        }

        return result;
    }
}
