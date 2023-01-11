using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class TourListVM : BaseVM
{
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    readonly string baseTitle = "Tours";
    readonly int limitItems = 20;
    int startItems = 0;
    int totalItems = 0;

    public ObservableCollection<Models.Tour> Tours { get; } = new();

    public FilterVM Filter { get; set; }

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
                Title = BuildTitle();
            }
            else
            {
                ItemsRefreshThreshold = -1;
                startItems = 0;
            }
            isPopulated = value;
        }
    }

    public TourListVM(IConnectivity connectivity, IGeolocation geolocation)
    {
        IsBusy = false;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    public async void PopulateData()
    {
        Title = baseTitle;
        await GetItems();
    }

    public void ClearData()
    {
        Tours.Clear();
        IsPopulated = false;
    }

    [RelayCommand]
    async Task GoToDetail(Tour tour)
    {
        if (tour == null)
            return;

        await Shell.Current.GoToAsync(nameof(TourDetailPage), true, new Dictionary<string, object>
        {
            {"Tour", tour}
        });
    }

    [RelayCommand]
    async Task GoToFilter()
    {
        await Shell.Current.GoToAsync(nameof(TourFilterPage), true, new Dictionary<string, object>
        {
            {"VM", this }
        });
    }

    [RelayCommand]
    async Task GetClosest()
    {
        if (IsBusy || Tours.Count == 0)
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
            var first = Tours.OrderBy(m => location.CalculateDistance(
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
            ResultTours result;
            Park park;
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

            //using var stream = await FileSystem.OpenAppPackageFileAsync("tours_0.json");
            //result = System.Text.Json.JsonSerializer.Deserialize<ResultTours>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //foreach (var tour in result.Data)
            //    Tours.Add(tour);

            result = await DataService.GetToursAsync(startItems, limitItems, states);
            startItems += result.Data.Count;
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

    private string BuildTitle()
    {
        string tmp = $"{baseTitle} ({totalItems}";
        if (Filter is not null && Filter.IsFiltered)
        {
            tmp += $", filtered";
        }
        tmp += ")";

        return tmp;
    }
}
