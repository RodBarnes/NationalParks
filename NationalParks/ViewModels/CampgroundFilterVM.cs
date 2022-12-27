namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(StatesCol), "States")]
    [QueryProperty(nameof(CampgroundVM), "VM")]
    public partial class CampgroundFilterVM : BaseVM
    {
        // Query properties
        public Collection<Models.State> StatesCol { get; set; }
        public CampgroundListVM CampgroundVM { get; set; }

        // Displayed values
        public ObservableCollection<Models.State> States { get; } = new();

        // Selected values
        public ObservableCollection<object> SelectedStates { get; set; } = new();

        public CampgroundFilterVM()
        {
            Title = "Filter";
        }

        public void PopulateData()
        {
            // Populate the available items
            foreach (var state in StatesCol)
            {
                States.Add(state);
            }

            // Populate the selected items
            foreach (var state in CampgroundVM.Filter.States)
            {
                SelectedStates.Add(state);
            }
        }
        [RelayCommand]
        async Task ApplyFilter()
        {
            // Update the filter
            CampgroundVM.Filter.States.Clear();
            foreach (var o in SelectedStates)
            {
                if (o is Models.State state)
                {
                    CampgroundVM.Filter.States.Add(state);
                }
            }

            // Clear the list
            CampgroundVM.ClearData();

            await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
            {
                {"Filter", CampgroundVM.Filter }
            });
        }

        [RelayCommand]
        public void ClearFilter()
        {
            // Clear the selections
            SelectedStates.Clear();

            // Clear the filter
            CampgroundVM.Filter.States.Clear();

            // Clear the list
            CampgroundVM.ClearData();

            Shell.Current.DisplayAlert("Filter", "All filter values have been cleared.", "OK");
        }
    }
}
