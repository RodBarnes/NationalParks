namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Campground), "Model")]
public partial class CampgroundDetailVM : DetailVM
{
    [ObservableProperty] Campground campground;
    [ObservableProperty] FeesVM feesVM;
    [ObservableProperty] OperatingHoursVM operatingHoursVM;
    [ObservableProperty] ContactsVM contactsVM;
    [ObservableProperty] CampsitesVM campsitesVM;
    [ObservableProperty] AmenitiesVM amenitiesVM;
    [ObservableProperty] CollapsibleViewVM accessibilityVM;
    [ObservableProperty] DirectionsVM directionsVM;
    [ObservableProperty] CollapsibleTextVM weatherVM;
    [ObservableProperty] CollapsibleTextVM reservationsVM;
    [ObservableProperty] CollapsibleTextVM regulationsVM;

    public CampgroundDetailVM(IMap map) : base(map)
    {
        Title = "Campground";
    }

    [RelayCommand]
    public void PopulateData()
    {
        AccessibilityVM = new CollapsibleViewVM("Accessibility", false);

        WeatherVM = new CollapsibleTextVM("Weather", false, Campground.WeatherOverview);
        ReservationsVM = new CollapsibleTextVM("Reservations", false, Campground.ReservationInfo, Campground.ReservationUrl);
        RegulationsVM = new CollapsibleTextVM("Regulations", false, Campground.RegulationsOverview, Campground.RegulationsUrl);

        DirectionsVM = new DirectionsVM("Directions", false, Campground.PhysicalAddress?.ToString(), Campground.DirectionsOverview);
        ContactsVM = new ContactsVM("Contacts", false, Campground.Contacts.PhoneNumbers, Campground.Contacts.EmailAddresses);
        FeesVM = new FeesVM("Fees", false, Campground.Fees);
        OperatingHoursVM = new OperatingHoursVM("Operating Hours", false, Campground.OperatingHours);
        CampsitesVM = new CampsitesVM("Campsites", false, Campground);
        AmenitiesVM = new AmenitiesVM("Amenities", false, Campground);
    }
}
