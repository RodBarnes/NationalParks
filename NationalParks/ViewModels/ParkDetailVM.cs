namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Park")]
public partial class ParkDetailVM : BaseVM
{
    [ObservableProperty]
    Park park;

    public ObservableCollection<Models.Topic> Topics { get; } = new();
    public ObservableCollection<Models.Activity> Activities { get; } = new();
    public ObservableCollection<Models.Fee> EntranceFees { get; } = new();

    [ObservableProperty]
    public CollapsibleViewVM topicsVM;

    [ObservableProperty]
    string activitiesIcon;

    [ObservableProperty]
    string isActivitiesVisible;

    [ObservableProperty]
    string entranceFeesIcon;

    [ObservableProperty]
    string isEntranceFeesVisible;

    [ObservableProperty]
    string directionsIcon;

    [ObservableProperty]
    string isDirectionsVisible;

    [ObservableProperty]
    string weatherIcon;

    [ObservableProperty]
    string isWeatherVisible;

    IMap map;

    public ParkDetailVM(IMap map)
    {
        Title = "Park";
        this.map = map;

        TopicsVM = new CollapsibleViewVM("Topics", false);

        ToggleActivities();
        ToggleEntranceFees();
        ToggleDirections();
        ToggleWeather();
    }

    [RelayCommand]
    public void ToggleTopics()
    {
        TopicsVM.Toggle();
    }

    [RelayCommand]
    public void ToggleActivities()
    {
        if (ActivitiesIcon == "arrow_down_green")
        {
            ActivitiesIcon = "arrow_up_green";
            IsActivitiesVisible = "True";
        }
        else
        {
            ActivitiesIcon = "arrow_down_green";
            IsActivitiesVisible = "False";
        }
    }

    [RelayCommand]
    public void ToggleEntranceFees()
    {
        if (EntranceFeesIcon == "arrow_down_green")
        {
            EntranceFeesIcon = "arrow_up_green";
            IsEntranceFeesVisible = "True";
        }
        else
        {
            EntranceFeesIcon = "arrow_down_green";
            IsEntranceFeesVisible = "False";
        }
    }

    [RelayCommand]
    public void ToggleDirections()
    {
        if (DirectionsIcon == "arrow_down_green")
        {
            DirectionsIcon = "arrow_up_green";
            IsDirectionsVisible = "True";
        }
        else
        {
            DirectionsIcon = "arrow_down_green";
            IsDirectionsVisible = "False";
        }
    }

    [RelayCommand]
    public void ToggleWeather()
    {
        if (WeatherIcon == "arrow_down_green")
        {
            WeatherIcon = "arrow_up_green";
            IsWeatherVisible = "True";
        }
        else
        {
            WeatherIcon = "arrow_down_green";
            IsWeatherVisible = "False";
        }
    }

    [RelayCommand]
    async Task OpenMap()
    {
        try
        {
            await map.OpenAsync(Park.DLatitude, Park.DLongitude, new MapLaunchOptions
            {
                Name = Park.Name,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to launch maps: {ex.Message}");
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task GoToHours()
    {
        await Shell.Current.GoToAsync(nameof(ParkHoursPage), true, new Dictionary<string, object>
        {
            {"Park", Park }
        });
    }

    [RelayCommand]
    async Task GoToImages()
    {
        await Shell.Current.GoToAsync(nameof(ParkImageListPage), true, new Dictionary<string, object>
        {
            {"Park", Park }
        });
    }

    public void PopulateData()
    {
        foreach (var topic in Park.Topics)
            Topics.Add(topic);

        foreach (var activity in Park.Activities)
            Activities.Add(activity);

        foreach (var entranceFee in Park.EntranceFees)
            EntranceFees.Add(entranceFee);
    }
}
