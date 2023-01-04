using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Filter), "Filter")]
    public partial class CampgroundListVM : BaseVM
    {
        public ObservableCollection<Models.Campground> Campgrounds { get; } = new();

        [ObservableProperty]
        bool isRefreshing;

        // For holding the available filter selections
        private Collection<Models.State> States { get; } = new();

        readonly DataService dataService;
        readonly IConnectivity connectivity;
        readonly IGeolocation geolocation;

        private int startCampgrounds = 0;
        private int limitCampgrounds = 20;
        private int totalCampgrounds = 0;

        public CampgroundListVM(DataService dataService, IConnectivity connectivity, IGeolocation geolocation)
        {
            IsBusy = false;
            Title = "Campgrounds";
            this.dataService = dataService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;

            LoadFilterDataAsync();
        }

        public Filter Filter { get; set; } = new Filter();

        public async void PopulateData()
        {
            await GetCampgroundsAsync();
            Title = $"Campgrounds ({totalCampgrounds})";
        }

        public void ClearData()
        {
            Campgrounds.Clear();
            startCampgrounds = 0;
        }

        private async void LoadFilterDataAsync()
        {
            await LoadStates();
        }

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
            await Shell.Current.GoToAsync(nameof(CampgroundFilterPage), true, new Dictionary<string, object>
        {
            {"States", States},
            {"VM", this }
        });
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
                string states = "";

                //using var stream = await FileSystem.OpenAppPackageFileAsync("campgrounds_0.json");
                //result = System.Text.Json.JsonSerializer.Deserialize<ResultCampgrounds>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                //foreach (var campground in result.Data)
                //    Campgrounds.Add(campground);

                foreach (var state in Filter.States)
                {
                    if (states.Length > 0)
                    {
                        states += ",";
                    }
                    states += state.Abbreviation;
                }

                result = await dataService.GetCampgroundsAsync(startCampgrounds, limitCampgrounds, states);
                startCampgrounds += result.Data.Count;
                foreach (var campground in result.Data)
                    Campgrounds.Add(campground);
                if (!int.TryParse(result.Total, out totalCampgrounds))
                {
                    totalCampgrounds = 0;
                }
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

        async Task LoadStates()
        {
            if (States?.Count > 0)
                return;

            using var stream = await FileSystem.OpenAppPackageFileAsync("states_titlecase.json");
            var result = JsonSerializer.Deserialize<ResultStates>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (result != null)
            {
                foreach (var item in result.Data)
                {
                    States.Add(item);
                }
            }
        }
    }
}
