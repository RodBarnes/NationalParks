namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(TopicsCol), "Topics")]
    [QueryProperty(nameof(ActivitiesCol), "Activities")]
    [QueryProperty(nameof(StatesCol), "States")]
    [QueryProperty(nameof(ParentVM), "VM")]
    public partial class ParkFilterVM : BaseVM
    {
        // Query properties
        public Collection<Models.Topic> TopicsCol { get; set; }
        public Collection<Models.Activity> ActivitiesCol { get; set; }
        public Collection<Models.State> StatesCol { get; set; }
        public ParkListVM ParentVM { get; set; }

        public ObservableCollection<Models.Topic> Topics { get; } = new();
        public ObservableCollection<Models.Activity> Activities { get; } = new();
        public ObservableCollection<Models.State> States { get; } = new();

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
