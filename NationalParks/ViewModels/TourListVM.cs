using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class TourListVM : ListVM
{
    readonly IConnectivity connectivity;

    public ObservableCollection<Models.Tour> Tours { get; } = new();

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
        Tours.Clear();
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
            Park park;
            ResultTours result = await DataService.GetToursAsync(StartItems, LimitItems, StatesFilter);
            foreach (var tour in result.Data)
            {
                ResultParks resultPark = await DataService.GetParkForParkCodeAsync(tour.Park.ParkCode);
                if (resultPark.Data.Count == 1)
                {
                    park = resultPark.Data[0];
                    tour.Latitude = park.Latitude;
                    tour.Longitude = park.Longitude;
                }
                Tours.Add(tour);
            }

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
}
