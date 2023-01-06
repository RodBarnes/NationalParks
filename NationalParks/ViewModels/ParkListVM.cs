﻿using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ParkListVM : BaseVM
{
    // For holding the available filter selections
    private Collection<Models.Topic> Topics { get; } = new();
    private Collection<Models.Activity> Activities { get; } = new();
    private Collection<Models.State> States { get; } = new();

    readonly DataService dataService;
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    private int startItems = 0;
    private int limitItems = 20;
    private int totalItems = 0;

    public ObservableCollection<Models.Park> Parks { get; } = new();

    public Filter Filter { get; set; } = new Filter();

    [ObservableProperty]
    int itemsRefreshThreshold = -1;

    private bool isPopulated = false;
    public bool IsPopulated
    {
        get => isPopulated;
        set
        {
            if (value == true)
            {
                ItemsRefreshThreshold = 2;
            }
            isPopulated = value;
        }
    }

    public ParkListVM(DataService dataService, IConnectivity connectivity, IGeolocation geolocation)
    {
        IsBusy = false;
        Title = $"Parks";
        this.dataService = dataService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    public async void PopulateData()
    {
        await GetItemsAsync();
        await GetAllTopicsAsync();
        await GetAllActivitiesAsync();
        await LoadStates();

        IsPopulated = true;
    }

    public void ClearData()
    {
        Parks.Clear();
        startItems = 0;
    }

    [RelayCommand]
    async Task GoToDetail(Park park)
    {
        if (park == null)
            return;

        await Shell.Current.GoToAsync(nameof(ParkDetailPage), true, new Dictionary<string, object>
        {
            {"Park", park}
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
    async Task GetItemsAsync()
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

            // Apply any filters prior to getting the items
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

            //using var stream = await FileSystem.OpenAppPackageFileAsync("parks_0.json");
            //result = System.Text.Json.JsonSerializer.Deserialize<ResultParks>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //foreach (var park in result.Data)
            //    Parks.Add(park);

            result = await dataService.GetParksAsync(startItems, limitItems, topics, activities, states);
            startItems += result.Data.Count;
            foreach (var park in result.Data)
            {
                Parks.Add(park);
                await GetAlertsAsync(park);
            }
            if (!int.TryParse(result.Total, out totalItems))
            {
                totalItems = 0;
            }
            Title = $"Parks ({totalItems})";
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get data items: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    async Task GetAlertsAsync(Park park)
    {
        if (park.Alerts?.Count > 0)
            return;

        try
        {
            int startAlerts = 0;
            int totalAlerts = 1;
            int limitAlerts = 20;

            while (totalAlerts > startAlerts)
            {
                var result = await dataService.GetAlertsAsync(park.ParkCode, startAlerts, limitAlerts);

                if (!int.TryParse(result.Total, out totalAlerts))
                    totalAlerts = 0;

                startAlerts += result.Data.Count;
                foreach (var alert in result.Data)
                    park.Alerts.Add(alert);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get data items: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
    }

    async Task GetAllTopicsAsync()
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

    async Task GetAllActivitiesAsync()
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
