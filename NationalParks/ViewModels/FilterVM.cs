namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(TopicsCol), "Topics")]
    [QueryProperty(nameof(ActivitiesCol), "Activities")]
    [QueryProperty(nameof(StatesCol), "States")]
    [QueryProperty(nameof(MainVM), "VM")]
    public partial class FilterVM : BaseVM
    {
        [ObservableProperty]
        Collection<Models.Topic> topicsCol;

        [ObservableProperty]
        Collection<Models.Activity> activitiesCol;

        [ObservableProperty]
        Collection<Models.State> statesCol;

        [ObservableProperty]
        MainVM mainVM;

        public ObservableCollection<Models.Topic> Topics { get; } = new();
        public ObservableCollection<Models.Activity> Activities { get; } = new();
        public ObservableCollection<Models.State> States { get; } = new();

        public FilterVM()
        {
            Title = "Filter";
        }

        public void PopulateData()
        {
            // This is not preferred but it is faster to do this than to acquire them everytime from the server.
            // Currently, the lists are acquired (one time) and stored in main, then passed in upon navigation.
            foreach (var topic in topicsCol)
                Topics.Add(topic);

            foreach (var activity in activitiesCol)
                Activities.Add(activity);

            foreach (var state in statesCol)
                States.Add(state);
        }

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
    }
}
