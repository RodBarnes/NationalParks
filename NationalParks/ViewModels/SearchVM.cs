﻿using NationalParks.Services;

namespace NationalParks.ViewModels
{
    public partial class SearchVM : BaseVM
    {
        public ObservableCollection<Topic> Topics { get; } = new();
        public ObservableCollection<Models.Activity> Activities { get; } = new();

        readonly DataService dataService;
        readonly IConnectivity connectivity;

        private int startTopics = 0;
        private int startActivities = 0;

        public SearchVM(DataService dataService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "Search";
            this.dataService = dataService;
            this.connectivity = connectivity;

            LoadDataAsync();
        }

        private async void LoadDataAsync()
        {
            await GetTopicsAsync();
            await GetActivitiesAsync();
        }

        [ObservableProperty]
        Search search;

        [RelayCommand]
        async Task GetTopicsAsync()
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

        [RelayCommand]
        async Task GetActivitiesAsync()
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
    }
}
