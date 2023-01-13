namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Park")]
public partial class ParkDetailVM : DetailVM
{
    [ObservableProperty] Park park;
    [ObservableProperty] CollapsibleViewVM alertsVM;
    [ObservableProperty] CollapsibleViewVM combinedFeesVM;
    [ObservableProperty] CollapsibleViewVM operatingHoursVM;
    [ObservableProperty] CollapsibleViewVM contactsVM;
    [ObservableProperty] CollapsibleViewVM topicsVM;
    [ObservableProperty] CollapsibleViewVM activitiesVM;
    [ObservableProperty] CollapsibleViewVM directionsVM;
    [ObservableProperty] CollapsibleViewVM weatherVM;

    public ParkDetailVM(IMap map) : base(map)
    {
        Title = "Park";

        AlertsVM = new CollapsibleViewVM("Alerts", false);
        CombinedFeesVM = new CollapsibleViewVM("Entrance Fees", false);
        OperatingHoursVM = new CollapsibleViewVM("Operating Hours", false);
        ContactsVM = new CollapsibleViewVM("Contacts", false);
        TopicsVM = new CollapsibleViewVM("Topics", false);
        ActivitiesVM = new CollapsibleViewVM("Activities", false);
        DirectionsVM = new CollapsibleViewVM("Directions", false);
        WeatherVM = new CollapsibleViewVM("Weather", false);
    }

    public void PopulateData()
    {
        Model = Park;
    }
}
