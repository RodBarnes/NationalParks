namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Models.Campground), "Campground")]
    public partial class CampgroundDetailVM : BaseVM
    {
        [ObservableProperty]
        Campground campground;

        [ObservableProperty]
        public CollapsibleViewVM feesVM;

        [ObservableProperty]
        public CollapsibleViewVM operatingHoursVM;

        [ObservableProperty]
        public CollapsibleViewVM contactsVM;

        [ObservableProperty]
        public CollapsibleViewVM campsiteInfoVM;

        [ObservableProperty]
        public CollapsibleViewVM amenitiesVM;

        [ObservableProperty]
        public CollapsibleViewVM accessibilityVM;

        [ObservableProperty]
        public CollapsibleViewVM directionsVM;

        [ObservableProperty]
        public CollapsibleViewVM weatherVM;

        [ObservableProperty]
        public CollapsibleViewVM reservationsVM;

        [ObservableProperty]
        public CollapsibleViewVM regulationsVM;

        IMap map;

        public CampgroundDetailVM(IMap map)
        {
            Title = "Campground";
            this.map = map;

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
        async Task OpenMap()
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
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");
                await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task GoToImages()
        {
            await Shell.Current.GoToAsync(nameof(CampgroundImageListPage), true, new Dictionary<string, object>
            {
                {"Campground", Campground }
            });
        }
    }
}
