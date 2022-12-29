namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Models.Campground), "Campground")]
    public partial class CampgroundDetailVM : BaseVM
    {
        [ObservableProperty]
        Campground campground;

        [ObservableProperty]
        string detailsIcon;

        [ObservableProperty]
        string isDetailsVisible;

        [ObservableProperty]
        string directionsIcon;

        [ObservableProperty]
        string isDirectionsVisible;

        [ObservableProperty]
        string weatherIcon;

        [ObservableProperty]
        string isWeatherVisible;

        [ObservableProperty]
        string reservationsIcon;

        [ObservableProperty]
        string isReservationsVisible;

        IMap map;

        public CampgroundDetailVM(IMap map)
        {
            Title = "Campground";
            this.map = map;
            ToggleDetails();
            ToggleDirections();
            ToggleWeather();
            ToggleReservations();
        }

        [RelayCommand]
        public void ToggleDetails()
        {
            if (DetailsIcon == "arrow_down_green")
            {
                DetailsIcon = "arrow_up_green";
                IsDetailsVisible = "True";
            }
            else
            {
                DetailsIcon = "arrow_down_green";
                IsDetailsVisible = "False";
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
        public void ToggleReservations()
        {
            if (ReservationsIcon == "arrow_down_green")
            {
                ReservationsIcon = "arrow_up_green";
                IsReservationsVisible = "True";
            }
            else
            {
                ReservationsIcon = "arrow_down_green";
                IsReservationsVisible = "False";
            }
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
        async Task GoToHours()
        {
            await Shell.Current.GoToAsync(nameof(CampgroundHoursPage), true, new Dictionary<string, object>
            {
                {"Campground", Campground }
            });
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
