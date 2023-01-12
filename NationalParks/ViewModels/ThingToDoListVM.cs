using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ThingToDoListVM : BaseVM
{
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    readonly string baseTitle = "Things To Do";
    readonly int limitItems = 20;
    int startItems = 0;
    int totalItems = 0;

    public ObservableCollection<Models.ThingToDo> ThingsToDo { get; } = new();

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
    public ThingToDoListVM(IConnectivity connectivity, IGeolocation geolocation)
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
        ThingsToDo.Clear();
        IsPopulated = false;
    }

    [RelayCommand]
    async Task GoToDetail(ThingToDo thingToDo)
    {
        if (thingToDo == null)
            return;

        await Shell.Current.GoToAsync(nameof(ThingToDoDetailPage), true, new Dictionary<string, object>
        {
            {"ThingToDo", thingToDo}
        });
    }

    [RelayCommand]
    async Task GoToFilter()
    {
        await Shell.Current.GoToAsync(nameof(ThingToDoFilterPage), true, new Dictionary<string, object>
        {
            {"VM", this }
        });
    }

    [RelayCommand]
    async Task GetClosest()
    {
        if (IsBusy || ThingsToDo.Count == 0)
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
            var first = ThingsToDo.OrderBy(m => location.CalculateDistance(
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
            ResultThingsToDo result;
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

            result = await DataService.GetThingsToDoAsync(startItems, limitItems, states);
            startItems += result.Data.Count;
            foreach (var thingToDo in result.Data)
            {
                ThingsToDo.Add(thingToDo);
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
