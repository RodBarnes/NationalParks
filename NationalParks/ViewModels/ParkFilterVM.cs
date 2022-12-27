namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(TopicsCol), "Topics")]
    [QueryProperty(nameof(ActivitiesCol), "Activities")]
    [QueryProperty(nameof(StatesCol), "States")]
    [QueryProperty(nameof(Filter), "Filter")]
    public partial class ParkFilterVM : BaseVM
    {
        // Query properties
        public Collection<Models.Topic> TopicsCol { get; set; }
        public Collection<Models.Activity> ActivitiesCol { get; set; }
        public Collection<Models.State> StatesCol { get; set; }
        public ParkFilter Filter { get; set; }

        // Displayed values
        public ObservableCollection<Models.Topic> Topics { get; } = new();
        public ObservableCollection<Models.Activity> Activities { get; } = new();
        public ObservableCollection<Models.State> States { get; } = new();

        // Selected values
        public ObservableCollection<object> SelectedTopics { get; set; } = new();
        public ObservableCollection<object> SelectedActivities { get; set; } = new();
        public ObservableCollection<object> SelectedStates { get; set; } = new();

        public ParkFilterVM()
        {
            Title = "Filter";
        }

        public void PopulateData()
        {
            // This is not preferred but it is faster to do this than to acquire them everytime from the server.
            // Currently, the lists are acquired (one time) and stored in main, then passed in upon navigation.
            foreach (var topic in TopicsCol)
                Topics.Add(topic);

            foreach (var activity in ActivitiesCol)
                Activities.Add(activity);

            foreach (var state in StatesCol)
                States.Add(state);
        }

        [RelayCommand]
        async Task ApplyFilter()
        {
            foreach (var o in SelectedTopics)
                if (o is Models.Topic topic)
                    Filter.Topics.Add(topic);

            foreach (var o in SelectedActivities)
                if (o is Models.Activity activity)
                    Filter.Activities.Add(activity);

            foreach (var o in SelectedStates)
                if (o is Models.State state)
                    Filter.States.Add(state);

            //await Shell.Current.DisplayAlert("Filter", "All filter values have been applied.", "OK");

            await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
            {
                {"Filter", Filter }
            });
        }

        [RelayCommand]
        public void ClearFilter()
        {
            SelectedTopics.Clear();
            SelectedActivities.Clear();
            SelectedStates.Clear();

            Shell.Current.DisplayAlert("Filter", "All filter values have been cleared.", "OK");
        }
    }
}
