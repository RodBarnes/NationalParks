using NationalParks.Services;

namespace NationalParks.ViewModels
{
    public partial class CampgroundListVM : BaseVM
    {
        public ObservableCollection<Models.Campground> Campgrounds { get; } = new();

        readonly DataService dataService;
        readonly IConnectivity connectivity;
        readonly IGeolocation geolocation;

        private int startCampgrounds = 0;

        public CampgroundListVM(DataService dataService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "Campgrounds";
            this.dataService = dataService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GoToDetail(Campground campground)
        {
            if (campground == null)
                return;

            await Shell.Current.GoToAsync(nameof(CampgroundDetailPage), true, new Dictionary<string, object>
            {
                {"Campground", campground }
            });
        }

        [RelayCommand]
        async Task GoToFilter()
        {
            await Shell.Current.DisplayAlert("Filter", $"How would GoToFilter() work for {this}?", "OK");
        }

        [RelayCommand]
        async Task GetClosestAsync()
        {
            if (IsBusy || Campgrounds.Count == 0)
                return;

            try
            {
                // Get cached location, else get real location.
                var location = await geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                // Find closest item to us
                var first = Campgrounds.OrderBy(m => location.CalculateDistance(
                    new Location(m.DLatitude, m.DLongitude), DistanceUnits.Miles))
                    .FirstOrDefault();

                await GoToDetail(first);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to query location: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
            }
        }

        [RelayCommand]
        async Task GetCampgroundsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;
                ResultCampgrounds result;

                //using var stream = await FileSystem.OpenAppPackageFileAsync("campgrounds.json");
                //result = System.Text.Json.JsonSerializer.Deserialize<ResultCampgrounds>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                //foreach (var campground in result.Data)
                //    Campgrounds.Add(campground);

                result = await dataService.GetCampgroundsAsync(startCampgrounds);
                startCampgrounds += result.Data.Count;
                foreach (var campground in result.Data)
                    Campgrounds.Add(campground);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get data items: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", $"{ex.Source}: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}
