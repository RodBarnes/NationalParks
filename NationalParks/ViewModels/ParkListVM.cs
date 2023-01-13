using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ParkListVM : ListVM
{
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    public ObservableCollection<Models.Park> Parks { get; } = new();

    public ParkListVM(IConnectivity connectivity, IGeolocation geolocation)
    {
        IsBusy = false;
        BaseTitle = "Parks";
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    public void ClearData()
    {
        Parks.Clear();
        IsPopulated = false;
    }

    [RelayCommand]
    async Task GoToDetail(Park park)
    {
        if (park == null)
            return;

        await Shell.Current.GoToAsync(nameof(ParkDetailPage), true, new Dictionary<string, object>
        {
            {"Model", park}
        });
    }

    [RelayCommand]
    async Task GetClosest()
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
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
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
            ResultParks result;
            string states = "";
            string topics = "";
            string activities = "";

            if (Filter is not null)
            {
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
            }

            //using var stream = await FileSystem.OpenAppPackageFileAsync("parks_0.json");
            //result = System.Text.Json.JsonSerializer.Deserialize<ResultParks>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //foreach (var park in result.Data)
            //    Parks.Add(park);

            result = await DataService.GetParksAsync(startItems, limitItems, topics, activities, states);
            startItems += result.Data.Count;
            foreach (var park in result.Data)
            {
                Parks.Add(park);
                await ParkListVM.GetAlerts(park);
            }
            totalItems = result.Total;
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

    static async Task GetAlerts(Park park)
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
                var result = await DataService.GetAlertsForParkCodeAsync(park.ParkCode, startAlerts, limitAlerts);
                totalAlerts = result.Total;
                startAlerts += result.Data.Count;
                foreach (var alert in result.Data)
                    park.Alerts.Add(alert);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
    }
}
