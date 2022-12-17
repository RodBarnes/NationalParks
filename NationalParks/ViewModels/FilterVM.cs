using NationalParks.Services;

namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(TopicsCol), "Topics")]
    [QueryProperty(nameof(ActivitiesCol), "Activities")]
    [QueryProperty(nameof(StatesCol), "States")]
    public partial class FilterVM : BaseVM
    {
        public ObservableCollection<Topic> Topics { get; } = new();
        public ObservableCollection<Models.Activity> Activities { get; } = new();
        public ObservableCollection<State> States { get; } = new();

        [ObservableProperty]
        Collection<Models.Topic> topicsCol;

        [ObservableProperty]
        Collection<Models.Activity> activitiesCol;

        [ObservableProperty]
        Collection<Models.State> statesCol;

        public FilterVM()
        {
            Title = "Filter";
        }

        public void PopulateCollections()
        {
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
