using NationalParks.Services;

namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(ParkVM), "VM")]
    public partial class ParkFilterVM : BaseVM
    {
        readonly DataService dataService;

        // Query properties
        public ParkListVM ParkVM { get; set; }

        // Selected values
        public ObservableCollection<object> SelectedTopics { get; set; } = new();
        public ObservableCollection<object> SelectedActivities { get; set; } = new();
        public ObservableCollection<object> SelectedStates { get; set; } = new();

        public ParkFilterVM(DataService dataService)
        {
            Title = "Filter";
            this.dataService = dataService;
        }

        public void PopulateData()
        {
            if (ParkVM.Filter is null)
                ParkVM.Filter = new FilterVM(dataService);

            // Populate the selected items
            foreach (var topic in ParkVM.Filter.Topics)
            {
                SelectedTopics.Add(topic);
            }
            foreach (var activity in ParkVM.Filter.Activities)
            {
                SelectedActivities.Add(activity);
            }
            foreach (var state in ParkVM.Filter.States)
            {
                SelectedStates.Add(state);
            }
        }

        [RelayCommand]
        async Task ApplyFilter()
        {
            // Update the filter
            ParkVM.Filter.Topics.Clear();
            foreach (var o in SelectedTopics)
            {
                if (o is Models.Topic topic)
                {
                    ParkVM.Filter.Topics.Add(topic);
                }
            }
            ParkVM.Filter.Activities.Clear();
            foreach (var o in SelectedActivities)
            {
                if (o is Models.Activity activity)
                {
                    ParkVM.Filter.Activities.Add(activity);
                }
            }
            ParkVM.Filter.States.Clear();
            foreach (var o in SelectedStates)
            {
                if (o is Models.State state)
                {
                    ParkVM.Filter.States.Add(state);
                }
            }

            // Clear the list
            ParkVM.ClearData();

            await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
            {
                {"Filter", ParkVM.Filter }
            });
        }

        [RelayCommand]
        public void ClearFilter()
        {
            // Clear the selections
            SelectedTopics.Clear();
            SelectedActivities.Clear();
            SelectedStates.Clear();

            // Clear the filter
            ParkVM.Filter.Topics.Clear();
            ParkVM.Filter.Activities.Clear();
            ParkVM.Filter.States.Clear();

            // Clear the list
            ParkVM.ClearData();

            Shell.Current.DisplayAlert("Filter", "All filter values have been cleared.", "OK");
        }
    }
}
