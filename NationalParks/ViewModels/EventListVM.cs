using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class EventListVM : BaseVM
{
    public ObservableCollection<Models.Event> Events { get; } = new();

    [ObservableProperty]
    bool isRefreshing;

    // For holding the available filter selections
    private Collection<Models.State> States { get; } = new();

    readonly DataService dataService;
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    private int startItems = 0;
    private int limitItems = 20;
    private int totalItems = 0;

    public bool IsPopulated { get; set; }

    public Filter Filter { get; set; } = new Filter();

    public EventListVM(DataService dataService, IConnectivity connectivity, IGeolocation geolocation)
    {
        IsBusy = false;
        Title = "Events";
        this.dataService = dataService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    public async void PopulateData()
    {
        // The RemainingItemsThresholdReachedCommand of the CollectionView will invoke this
        // upon first displaying the page.  If that property is removed from the CollectionView, this
        // explicit invocation is required.
        //await GetEventsAsync();
        await LoadStates();

        IsPopulated = true;
    }

    public void ClearData()
    {
        Events.Clear();
        startItems = 0;
    }

    [RelayCommand]
    async Task GoToDetail(Event eventx)
    {
        if (eventx == null)
            return;

        await Shell.Current.GoToAsync(nameof(EventDetailPage), true, new Dictionary<string, object>
        {
            {"Event", eventx}
        });
    }

    [RelayCommand]
    async Task GetClosestAsync()
    {
        if (IsBusy || Events.Count == 0)
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
            var first = Events.OrderBy(m => location.CalculateDistance(
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
    async Task GetEventsAsync()
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
            ResultEvents result;
            string states = "";

            // Apply any filters prior to getting the items
            foreach (var state in Filter.States)
            {
                if (states.Length > 0)
                {
                    states += ",";
                }
                states += state.Abbreviation;
            }

            //using var stream = await FileSystem.OpenAppPackageFileAsync("events_0.json");
            //result = System.Text.Json.JsonSerializer.Deserialize<ResultEvents>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //foreach (var event in result.Data)
            //    Events.Add(event);

            result = await dataService.GetEventsAsync(startItems, limitItems, states);
            startItems += result.Data.Count;
            foreach (var eventx in result.Data)
                Events.Add(eventx);

            if (!int.TryParse(result.Total, out totalItems))
            {
                totalItems = 0;
            }
            Title = $"Events ({totalItems})";
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get data items: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
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
