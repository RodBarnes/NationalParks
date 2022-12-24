using NationalParks.Services;

namespace NationalParks.ViewModels
{
    public partial class CampgroundListVM : BaseVM
    {
        public ObservableCollection<Models.Campground> Campgrounds { get; } = new();

        readonly DataService dataService;
        readonly IConnectivity connectivity;

        private int startCampgrounds = 0;

        public CampgroundListVM(DataService dataService, IConnectivity connectivity)
        {
            Title = "Campgrounds";
            this.dataService = dataService;
            this.connectivity = connectivity;
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
            await Shell.Current.DisplayAlert("Filter", $"How would GetClosest() work for {this}?", "OK");
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
                var result = await dataService.GetCampgroundsAsync(startCampgrounds);

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
