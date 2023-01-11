using NationalParks.Services;

namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Models.Campground), "Campground")]
    public partial class CampgroundDetailVM : BaseVM
    {
        IMap map;
        DataService dataService;

        [ObservableProperty] Campground campground;

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

        public CampgroundDetailVM(DataService dataService, IMap map)
        {
            Title = "Campground";
            this.map = map;
            this.dataService = dataService;

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

        [RelayCommand]
        async Task OpenMapAsync()
        {
            try
            {
                await map.OpenAsync(Campground.DLatitude, Campground.DLongitude, new MapLaunchOptions
                {
                    Name = Campground.Name,
                    NavigationMode = NavigationMode.None
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task GoToImagesAsync()
        {
            await Shell.Current.GoToAsync(nameof(CampgroundImageListPage), true, new Dictionary<string, object>
            {
                {"Campground", Campground }
            });
        }

        [RelayCommand]
        async Task GoToParkFromParkCodeAsync()
        {
            Park park;

            ResultParks result = await dataService.GetParkForParkCodeAsync(Campground.ParkCode);
            if (result.Data.Count == 1)
            {
                park = result.Data[0];
                await Shell.Current.GoToAsync(nameof(ParkDetailPage), true, new Dictionary<string, object>
                {
                    {"Park", park }
                });
            }
            else
            {
                await Shell.Current.DisplayAlert("Error!", "Unable to get park!", "OK");
            }
        }
    }
}
