using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class EventListVM : BaseVM
{
    // For holding the available filter selections
    private Collection<Models.State> States { get; } = new();

    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    private int startItems = 0;
    private int limitItems = 20;
    private int totalItems = 0;

    public ObservableCollection<Models.Event> Events { get; } = new();
    public FilterVM Filter { get; set; } = new FilterVM();

    [ObservableProperty] int itemsRefreshThreshold = -1;

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

    public EventListVM(IConnectivity connectivity, IGeolocation geolocation)
    {
        IsBusy = false;
        Title = "Events";
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    public async void PopulateData()
    {
        await GetItems();

        IsPopulated = true;
    }

    public void ClearData()
    {
        Events.Clear();
        startItems = 0;
    }

    [RelayCommand]
    async Task GoToDetail(Event npsEvent)
    {
        if (npsEvent == null)
            return;

        await Shell.Current.GoToAsync(nameof(EventDetailPage), true, new Dictionary<string, object>
        {
            {"Event", npsEvent}
        });
    }

    [RelayCommand]
    async Task GetClosest()
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

            result = await DataService.GetEventsAsync(startItems, limitItems, states);
            startItems += result.Data.Count;
            foreach (var npsEvent in result.Data)
                Events.Add(npsEvent);

            totalItems = result.Total;
            Title = $"Events ({totalItems})";
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
}
