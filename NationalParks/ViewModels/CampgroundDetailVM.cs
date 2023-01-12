namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Campground), "Campground")]
public partial class CampgroundDetailVM : DetailVM
{
    [ObservableProperty] Campground campground;

    [ObservableProperty]
    public Dictionary<string, object> openMapDict;

    [ObservableProperty]
    public Dictionary<string, object> goToImagesDict;

    [ObservableProperty] CollapsibleViewVM feesVM;

    [ObservableProperty] CollapsibleViewVM operatingHoursVM;

    [ObservableProperty] CollapsibleViewVM contactsVM;

    [ObservableProperty] CollapsibleViewVM campsiteInfoVM;

    [ObservableProperty] CollapsibleViewVM amenitiesVM;

    [ObservableProperty] CollapsibleViewVM accessibilityVM;

    [ObservableProperty] CollapsibleViewVM directionsVM;

    [ObservableProperty] CollapsibleViewVM weatherVM;

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
        DirectionsVM = new CollapsibleViewVM("Directions", false);
        WeatherVM = new CollapsibleViewVM("Weather", false);
        ReservationsVM = new CollapsibleViewVM("Reservations", false);
        RegulationsVM = new CollapsibleViewVM("Regulations", false);
    }

    public void PopulateData()
    {
        OpenMapDict = new Dictionary<string, object>
        {
            { "Latitude", Campground.DLatitude },
            { "Longitude", Campground.DLongitude },
            { "Name", Campground.Name }
        };

        GoToImagesDict = new Dictionary<string, object>
        {
            { "PageName", nameof(CampgroundImageListPage) },
            { "ParamName", "Images" },
            { "Object", Campground.Images }
        };
    }
}
