using NationalParks.Services;

namespace NationalParks.ViewModels
{
    [QueryProperty("TopicsFromMain", "Topics")]
    [QueryProperty("ActivitiesFromMain", "Activities")]
    [QueryProperty("StatesFromMain", "States")]
    public partial class FilterVM : BaseVM
    {
        public ObservableCollection<Topic> Topics { get; } = new();
        public ObservableCollection<Models.Activity> Activities { get; } = new();
        public ObservableCollection<State> States { get; } = new();

        [ObservableProperty]
        Collection<Models.Topic> topicsFromMain;

        [ObservableProperty]
        Collection<Models.Activity> activitiesFromMain;

        [ObservableProperty]
        Collection<Models.State> statesFromMain;

        public FilterVM()
        {
            Title = "Filter";
        }

        public void PopulateCollections()
        {
            foreach (var topic in topicsFromMain)
                Topics.Add(topic);

            foreach (var activity in activitiesFromMain)
                Activities.Add(activity);

            foreach (var state in statesFromMain)
                States.Add(state);
        }

        [RelayCommand]
        public void ApplyFilter()
        {
            Shell.Current.DisplayAlert("Filter",
                "This will go back to the main page with the list filtered.", "OK");

            PopulateCollections();
        }

        [RelayCommand]
        public void ClearFilter()
        {
            Shell.Current.DisplayAlert("Filter",
                "This will clear all selections and reset the filter.", "OK");
        }
    }
}
