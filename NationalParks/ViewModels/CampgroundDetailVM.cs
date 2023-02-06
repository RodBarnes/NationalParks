namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Campground), "Model")]
public partial class CampgroundDetailVM : DetailVM
{
    [ObservableProperty] Campground campground;
    [ObservableProperty] CollapsibleViewVM feesVM;
    [ObservableProperty] CollapsibleViewVM operatingHoursVM;
    [ObservableProperty] CollapsibleViewVM contactsVM;
    [ObservableProperty] CollapsibleViewVM campsiteInfoVM;
    [ObservableProperty] CollapsibleViewVM amenitiesVM;
    [ObservableProperty] CollapsibleViewVM accessibilityVM;
    [ObservableProperty] DirectionsVM directionsVM;
    [ObservableProperty] CollapsibleTextVM weatherVM;
    [ObservableProperty] CollapsibleViewVM reservationsVM;
    [ObservableProperty] CollapsibleViewVM regulationsVM;

    public CampgroundDetailVM(IMap map) : base(map)
    {
        Title = "Campground";
        FeesVM = new CollapsibleViewVM("Fees", false);
        OperatingHoursVM = new CollapsibleViewVM("Operating Hours", false);
        ContactsVM = new CollapsibleViewVM("Contacts", false);
        CampsiteInfoVM = new CollapsibleViewVM("Campsite Info", false);
        AmenitiesVM = new CollapsibleViewVM("Amenities", false);
        AccessibilityVM = new CollapsibleViewVM("Accessibility", false);
        DirectionsVM = new DirectionsVM("Directions", false);
        WeatherVM = new CollapsibleTextVM("Weather", false);
        ReservationsVM = new CollapsibleViewVM("Reservations", false);
        RegulationsVM = new CollapsibleViewVM("Regulations", false);
    }

    [RelayCommand]
    public void PopulateData()
    {
        DirectionsVM.HasContent = campground.HasDirections;
        DirectionsVM.PhysicalAddress = campground.PhysicalAddress?.ToString();
        DirectionsVM.Directions = campground.DirectionsOverview;
        WeatherVM.Text = campground.WeatherOverview;
        WeatherVM.HasContent = campground.HasWeather;
    }

}
