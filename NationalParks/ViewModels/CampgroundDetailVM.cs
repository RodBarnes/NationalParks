namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Campground), "Model")]
public partial class CampgroundDetailVM : DetailVM
{
    [ObservableProperty] Campground campground;
    [ObservableProperty] FeesVM feesVM;
    [ObservableProperty] OperatingHoursVM operatingHoursVM;
    [ObservableProperty] ContactsVM contactsVM;
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
        FeesVM = new FeesVM("Fees", false);
        OperatingHoursVM = new OperatingHoursVM("Operating Hours", false);
        ContactsVM = new ContactsVM("Contacts", false);
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
        FeesVM.HasContent = Campground.HasFees;
        FeesVM.Fees = Campground.Fees;
        OperatingHoursVM.HasContent = Campground.HasOperatingHours;
        OperatingHoursVM.OperatingHours = Campground.OperatingHours;
        ContactsVM.HasContent = Campground.HasContacts;
        ContactsVM.PhoneContacts = Campground.Contacts.PhoneNumbers;
        ContactsVM.EmailContacts = Campground.Contacts.EmailAddresses;
        DirectionsVM.HasContent = Campground.HasDirections;
        DirectionsVM.PhysicalAddress = Campground.PhysicalAddress?.ToString();
        DirectionsVM.Directions = Campground.DirectionsOverview;
        WeatherVM.HasContent = Campground.HasWeather;
        WeatherVM.Text = Campground.WeatherOverview;
    }

}
