using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels;

public partial class MainVM : BaseVM
{
    public ObservableCollection<Models.Park> Parks { get; } = new();

    private Collection<Models.Topic> Topics { get; } = new();
    private Collection<Models.Activity> Activities { get; } = new();
    private Collection<Models.State> States { get; } = new();

    readonly DataService dataService;
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    private int startParks = 0;

    public MainVM(DataService dataService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Parks";
        this.dataService = dataService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;

        LoadMainDataAsync();
    }
    
    private async void LoadMainDataAsync()
    {
        await GetParksAsync();
        await GetTopicsAsync();
        await GetActivitiesAsync();
        await LoadStates();
    }

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    Filter filter;

    [RelayCommand]
    async Task GoToDetails(Park park)
    {
        if (park == null)
        return;

        await Shell.Current.GoToAsync(nameof(ParkPage), true, new Dictionary<string, object>
        {
            {"Park", park }
        });
    }

    [RelayCommand]
    async Task GoToFilter()
    {
        await Shell.Current.GoToAsync(nameof(FilterPage), true, new Dictionary<string, object>
        {
            {"Topics", Topics },
            {"Activities", Activities },
            {"States", States},
            {"VM", this }
        });
    }

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
            var result = await dataService.GetParksAsync(startParks);

            startParks += result.Data.Count;
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

    async Task GetTopicsAsync()
    {
        if (IsBusy)
            return;

        if (Topics?.Count > 0)
            return;

        try
        {
            IsBusy = true;
            int startTopics = 0;
            int totalTopics = 1;

            while (totalTopics > startTopics)
            {
                var result = await dataService.GetTopicsAsync(startTopics);

                if (!int.TryParse(result.Total, out totalTopics))
                    totalTopics = 0;

                startTopics += result.Data.Count;
                foreach (var topic in result.Data)
                    Topics.Add(topic);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get data items: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    async Task GetActivitiesAsync()
    {
        if (IsBusy)
            return;

        if (Activities?.Count > 0)
            return;

        try
        {
            IsBusy = true;

            int startActivities = 0;
            int totalActivities = 1;

            while (totalActivities > startActivities)
            {
                var result = await dataService.GetActivitiesAsync(startActivities);

                if (!int.TryParse(result.Total, out totalActivities))
                    totalActivities = 0;

                startActivities += result.Data.Count;
                foreach (var activity in result.Data)
                    Activities.Add(activity);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get data items: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    async Task LoadStates()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("states_titlecase.json");
        var result = JsonSerializer.Deserialize<ResultStates>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        if (result != null)
        {
            foreach (var item in result.List)
            {
                States.Add(item);
            }
        }
    }
}
