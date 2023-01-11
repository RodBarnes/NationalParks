namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Park")]
public partial class ParkDetailVM : BaseVM
{
    IMap map;

    [ObservableProperty]
    Park park;

    [ObservableProperty] CollapsibleViewVM alertsVM;

    [ObservableProperty] CollapsibleViewVM combinedFeesVM;

    [ObservableProperty] CollapsibleViewVM operatingHoursVM;

    [ObservableProperty] CollapsibleViewVM contactsVM;

    [ObservableProperty] CollapsibleViewVM topicsVM;

    [ObservableProperty] CollapsibleViewVM activitiesVM;

    [ObservableProperty] CollapsibleViewVM directionsVM;

    [ObservableProperty] CollapsibleViewVM weatherVM;

    public ParkDetailVM(IMap map)
    {
        Title = "Park";
        this.map = map;

        AlertsVM = new CollapsibleViewVM("Alerts", false);
        CombinedFeesVM = new CollapsibleViewVM("Entrance Fees", false);
        OperatingHoursVM = new CollapsibleViewVM("Operating Hours", false);
        ContactsVM = new CollapsibleViewVM("Contacts", false);
        TopicsVM = new CollapsibleViewVM("Topics", false);
        ActivitiesVM = new CollapsibleViewVM("Activities", false);
        DirectionsVM = new CollapsibleViewVM("Directions", false);
        WeatherVM = new CollapsibleViewVM("Weather", false);
    }

    [RelayCommand]
    async Task OpenMap()
    {
        if (Park.DLatitude < 0)
        {
            await Shell.Current.DisplayAlert("No location", "Location coordinates are not provided.  Review the description for possible directions or related landmarks.", "OK");
            return;
        }

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
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task GoToImages()
    {
        await Shell.Current.GoToAsync(nameof(ParkImageListPage), true, new Dictionary<string, object>
        {
            {"Park", Park }
        });
    }
}
