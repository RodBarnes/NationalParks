using NationalParks.Services;

namespace NationalParks.ViewModels
{
    public partial class WebcamListVM : BaseVM
    {
        readonly DataService dataService;
        readonly IConnectivity connectivity;

        private int startWebcams = 0;
        public ObservableCollection<Models.Webcam> Webcams { get; } = new();

        [ObservableProperty]
        int itemsRefreshThreshold = -1;

        private bool isPopulated = false;
        public bool IsPopulated
        {
            get => isPopulated;
            set
            {
                if (value == true)
                {
                    ItemsRefreshThreshold = 2;
                }
                isPopulated = value;
            }
        }

        public WebcamListVM(DataService dataService, IConnectivity connectivity)
        {
            Title = "Webcams";
            this.dataService = dataService;
            this.connectivity = connectivity;
        }

        public async void PopulateData()
        {
            await GetItemsAsync();

            IsPopulated = true;
        }

        [RelayCommand]
        async Task GoToDetail(Webcam webcam)
        {
            if (webcam == null)
                return;

            await Shell.Current.GoToAsync(nameof(WebcamDetailPage), true, new Dictionary<string, object>
            {
                {"Webcam", webcam }
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
        async Task GetItemsAsync()
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
                ResultWebcams result;

                //using var stream = await FileSystem.OpenAppPackageFileAsync("webcams_0.json");
                //result = System.Text.Json.JsonSerializer.Deserialize<ResultWebcams>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                //foreach (var webcam in result.Data)
                //    Webcams.Add(webcam);

                result = await dataService.GetWebcamsAsync(startWebcams);
                startWebcams += result.Data.Count;
                foreach (var webcam in result.Data)
                    Webcams.Add(webcam);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get data items: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
