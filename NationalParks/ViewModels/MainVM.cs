using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class MainVM : BaseVM
{
    public ObservableCollection<Park> Parks { get; } = new();
    public ObservableCollection<Topic> Topics { get; } = new();
    public ObservableCollection<Models.Activity> Activities { get; } = new();

    readonly DataService dataService;
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    private int _startParks = 0;
    private int _startTopics = 0;
    private int _startActivities = 0;

    public MainVM(DataService dataService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Parks";
        this.dataService = dataService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;

        LoadDataAsync();
    }
    
    private async void LoadDataAsync()
    {
        await GetParksAsync();
        await GetTopicsAsync();
        await GetActivitiesAsync();
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
    async Task GetParksAsync()
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
            var result = await dataService.GetParksAsync(_startParks);

            _startParks += result.Data.Count;
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
    async Task GetTopicsAsync()
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
            var result = await dataService.GetTopicsAsync(_startTopics);

            _startTopics += result.Data.Count;
            foreach (var topic in result.Data)
                Topics.Add(topic);
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
    async Task GetActivitiesAsync()
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
            var result = await dataService.GetActivitiesAsync(_startActivities);

            _startActivities += result.Data.Count;
            foreach (var activity in result.Data)
                Activities.Add(activity);
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
    async Task GetClosestParkAsync()
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
                new Location(m.DLatitude, m.DLongitude), DistanceUnits.Miles))
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
