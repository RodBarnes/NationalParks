using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels
{
    public partial class FilterVM : BaseVM
    {
        public ObservableCollection<Topic> Topics { get; } = new();
        public ObservableCollection<Models.Activity> Activities { get; } = new();
        public ObservableCollection<State> States { get; } = new();

        readonly DataService dataService;
        readonly IConnectivity connectivity;

        private int startTopics = 0;
        private int startActivities = 0;

        public FilterVM(DataService dataService, IConnectivity connectivity)
        {
            Title = "Filter";
            this.dataService = dataService;
            this.connectivity = connectivity;

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    "Please check internet and try again.", "OK");
                return;
            }
            
            await GetTopicsAsync();
            await GetActivitiesAsync();
            await LoadStates();
        }

        [ObservableProperty]
        Filter search;

        [RelayCommand]
        public void ApplyFilter()
        {
            Shell.Current.DisplayAlert("Filter",
                "This will go back to the main page with the list filtered.", "OK");
        }

        [RelayCommand]
        public void ClearFilter()
        {
            Shell.Current.DisplayAlert("Filter",
                "This will clear all selections and reset the filter.", "OK");
        }

        async Task GetTopicsAsync()
        {
            if (IsBusy)
                return;

            if (Topics?.Count > 0)
                return;

            try
            {
                IsBusy = true;

                startActivities = 0;
                int totalTopics = 1;

                while (totalTopics > startTopics)
                {
                    var result = await dataService.GetTopicsAsync(startTopics);

                    if (!int.TryParse(result.Total, out totalTopics))
                        totalTopics = 0;

                    startTopics += result.Data.Count;
                    foreach (var topic in result.Data)
                        Topics.Add(topic);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get data items: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        async Task GetActivitiesAsync()
        {
            if (IsBusy)
                return;

            if (Activities?.Count > 0)
                return;

            try
            {
                IsBusy = true;

                startActivities = 0;
                int totalActivities = 1;

                while (totalActivities > startActivities)
                {
                    var result = await dataService.GetActivitiesAsync(startActivities);

                    if (!int.TryParse(result.Total, out totalActivities))
                        totalActivities = 0;

                    startActivities += result.Data.Count;
                    foreach (var activity in result.Data)
                        Activities.Add(activity);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get data items: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task LoadStates()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("states_titlecase.json");
            var result = JsonSerializer.Deserialize<ResultStates>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (result != null)
            {
                foreach (var item in result.List)
                {
                    States.Add(item);
                }
            }
        }
    }
}
