using NationalParks.Services;

namespace NationalParks.ViewModel;

public partial class MainVM : BaseVM
{
    public ObservableCollection<Park> Parks { get; } = new();

    DataService dataService;
    IConnectivity connectivity;
    IGeolocation geolocation;

    private int _start = 0;

    public MainVM(DataService dataService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Parks";
        this.dataService = dataService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }
    
    [RelayCommand]
    async Task GoToDetails(Park park)
    {
        if (park == null)
        return;

        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"Park", park }
        });
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task GetDataAsync()
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
            var result = await dataService.GetData(_start);

            _start += result.Data.Count;
            foreach (var park in result.Data)
                Parks.Add(park);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get data items: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }

    }

    [RelayCommand]
    async Task GetClosestAsync()
    {
        if (IsBusy || Parks.Count == 0)
            return;

        try
        {
            // Get cached location, else get real location.
            var location = await geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            // Find closest item to us
            var first = Parks.OrderBy(m => location.CalculateDistance(
                new Location(m.dLatitude, m.dLongitude), DistanceUnits.Miles))
                .FirstOrDefault();

            await Shell.Current.DisplayAlert("", first.Name + " " +
                first.Location, "OK");

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to query location: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
}
