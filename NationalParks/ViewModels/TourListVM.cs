using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class TourListVM : BaseVM
{
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    private int startItems = 0;
    private int limitItems = 20;
    private int totalItems = 0;

    public ObservableCollection<Models.Tour> Tours { get; } = new();

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

    public TourListVM(IConnectivity connectivity, IGeolocation geolocation)
    {
        IsBusy = false;
        Title = $"Tours";
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    public async void PopulateData()
    {
        await GetItemsAsync();

        IsPopulated = true;
    }

    public void ClearData()
    {
        Tours.Clear();
        startItems = 0;
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
        await Shell.Current.DisplayAlert("???", $"TourFilterPage", "OK");

        //await Shell.Current.GoToAsync(nameof(TourFilterPage), true, new Dictionary<string, object>
        //{
        //    {"Topics", Topics },
        //    {"Activities", Activities },
        //    {"States", States},
        //    {"VM", this }
        //});
    }

    [RelayCommand]
    async Task GetClosestAsync()
    {
        if (IsBusy || Tours.Count == 0)
            return;

        //try
        //{
        //    // Get cached location, else get real location.
        //    var location = await geolocation.GetLastKnownLocationAsync();
        //    if (location == null)
        //    {
        //        location = await geolocation.GetLocationAsync(new GeolocationRequest
        //        {
        //            DesiredAccuracy = GeolocationAccuracy.Medium,
        //            Timeout = TimeSpan.FromSeconds(30)
        //        });
        //    }

        //    // Find closest item to us
        //    var first = Tours.OrderBy(m => location.CalculateDistance(
        //        new Location(m.DLatitude, m.DLongitude), DistanceUnits.Miles))
        //        .FirstOrDefault();

        //    await GoToDetail(first);
        //}
        //catch (Exception ex)
        //{
        //    await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        //}
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
            ResultTours result;
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

            //using var stream = await FileSystem.OpenAppPackageFileAsync("tours_0.json");
            //result = System.Text.Json.JsonSerializer.Deserialize<ResultTours>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //foreach (var tour in result.Data)
            //    Tours.Add(tour);

            result = await DataService.GetToursAsync(startItems, limitItems, states);
            startItems += result.Data.Count;
            foreach (var tour in result.Data)
            {
                Tours.Add(tour);
            }
            totalItems = result.Total;
            Title = $"Tours ({totalItems})";
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
