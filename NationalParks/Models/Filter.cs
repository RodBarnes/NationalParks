using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.Models
{
    public class Filter
    {
        readonly DataService dataService;

        public static ObservableCollection<State> StateSelections { get; } = new();
        public static ObservableCollection<Topic> TopicSelections { get; } = new();
        public static ObservableCollection<Activity> ActivitySelections { get; } = new();

        public List<Topic> Topics { get; set; } = new();
        public List<Activity> Activities { get; set; } = new();
        public List<State> States { get; set; } = new();

        public Filter() { }

        public Filter(DataService dataService) : base()
        {
            this.dataService = dataService;

            PopulateData();
        }

        public async Task PopulateData()
        {
            // Populate the available selections
            await LoadStates();
            await GetAllActivitiesAsync();
            await GetAllTopicsAsync();
        }

        public async Task GetAllTopicsAsync()
        {
            if (TopicSelections?.Count > 0)
                return;

            try
            {
                int startTopics = 0;
                int totalTopics = 1;

                while (totalTopics > startTopics)
                {
                    var result = await dataService.GetTopicsAsync(startTopics);

                    if (!int.TryParse(result.Total, out totalTopics))
                        totalTopics = 0;

                    startTopics += result.Data.Count;
                    foreach (var topic in result.Data)
                        TopicSelections.Add(topic);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get data items: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
            }
        }

        public async Task GetAllActivitiesAsync()
        {
            if (ActivitySelections?.Count > 0)
                return;

            try
            {
                int startActivities = 0;
                int totalActivities = 1;

                while (totalActivities > startActivities)
                {
                    var result = await dataService.GetActivitiesAsync(startActivities);

                    if (!int.TryParse(result.Total, out totalActivities))
                        totalActivities = 0;

                    startActivities += result.Data.Count;
                    foreach (var activity in result.Data)
                        ActivitySelections.Add(activity);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get data items: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
            }
        }

        public async Task LoadStates()
        {
            if (StateSelections?.Count > 0)
                return;

            using var stream = await FileSystem.OpenAppPackageFileAsync("states_titlecase.json");
            var result = JsonSerializer.Deserialize<ResultStates>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            if (result != null)
            {
                foreach (var item in result.Data)
                {
                    StateSelections.Add(item);
                }
            }
        }
    }
}
