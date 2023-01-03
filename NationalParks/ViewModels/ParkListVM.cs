using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ParkListVM : BaseVM
{
    public ObservableCollection<Models.Park> Parks { get; } = new();

    [ObservableProperty]
    bool isRefreshing;

    // For holding the available filter selections
    private Collection<Models.ParkTopic> Topics { get; } = new();
    private Collection<Models.ParkActivity> Activities { get; } = new();
    private Collection<Models.State> States { get; } = new();

    readonly DataService dataService;
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    private int startParks = 0;
    private int limitParks = 20;
    private int totalParks = 0;

    public ParkListVM(DataService dataService, IConnectivity connectivity, IGeolocation geolocation)
    {
        IsBusy = false;
        Title = $"Parks";
        this.dataService = dataService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;

        LoadFilterDataAsync();
    }

    public ParkFilter Filter { get; set; } = new ParkFilter();

    public async void PopulateData()
    {
        await GetParksAsync();
        Title = $"Parks ({totalParks})";
    }

    public void ClearData()
    {
        Parks.Clear();
        startParks = 0;
    }

    private async void LoadFilterDataAsync()
    {
        await GetTopicsAsync();
        await GetActivitiesAsync();
        await LoadStates();
    }

    [RelayCommand]
    async Task GoToDetail(Park park)
    {
        if (park == null)
        return;

        await Shell.Current.GoToAsync(nameof(ParkDetailPage), true, new Dictionary<string, object>
        {
            {"Park", park }
        });
    }

    [RelayCommand]
    async Task GoToFilter()
    {
        await Shell.Current.GoToAsync(nameof(ParkFilterPage), true, new Dictionary<string, object>
        {
            {"Topics", Topics },
            {"Activities", Activities },
            {"States", States},
            {"VM", this }
        });
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
                new Location(m.DLatitude, m.DLongitude), DistanceUnits.Miles))
                .FirstOrDefault();

            await GoToDetail(first);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to query location: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
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
            ResultParks result;
            string states = "";
            string topics = "";
            string activities = "";

            //using var stream = await FileSystem.OpenAppPackageFileAsync("parks_0.json");
            //result = System.Text.Json.JsonSerializer.Deserialize<ResultParks>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //foreach (var park in result.Data)
            //    Parks.Add(park);

            foreach (var topic in Filter.Topics)
            {
                if (topics.Length > 0)
                {
                    topics += "%2D";
                }
                topics += topic.Id;
            }

            foreach (var activity in Filter.Activities)
            {
                if (activities.Length > 0)
                {
                    activities += "%2D";
                }
                activities += activity.Id;
            }

            foreach (var state in Filter.States)
            {
                if (states.Length > 0)
                {
                    states += ",";
                }
                states += state.Abbreviation;
            }

            result = await dataService.GetParksAsync(startParks, limitParks, topics, activities, states);
            startParks += result.Data.Count;
            foreach (var park in result.Data)
            {
                Parks.Add(park);
            }
            if (!int.TryParse(result.Total, out totalParks))
            {
                totalParks = 0;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get data items: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }

    }

    async Task GetTopicsAsync()
    {
        if (Topics?.Count > 0)
            return;

        try
        {
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
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
    }

    async Task GetActivitiesAsync()
    {
        if (Activities?.Count > 0)
            return;

        try
        {
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
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
    }

    async Task LoadStates()
    {
        if (States?.Count > 0)
            return;

        using var stream = await FileSystem.OpenAppPackageFileAsync("states_titlecase.json");
        var result = JsonSerializer.Deserialize<ResultStates>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        if (result != null)
        {
            foreach (var item in result.Data)
            {
                States.Add(item);
            }
        }
    }
}
