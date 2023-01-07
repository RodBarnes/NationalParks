using NationalParks.Services;

namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(CampgroundVM), "VM")]
    public partial class CampgroundFilterVM : BaseVM
    {
        readonly DataService dataService;

        // Query properties
        public CampgroundListVM CampgroundVM { get; set; }

        // Selected values
        public ObservableCollection<object> SelectedStates { get; set; } = new();

        public CampgroundFilterVM(DataService dataService)
        {
            Title = "Filter";
            this.dataService = dataService;
        }

        public void PopulateData()
        {
            if (CampgroundVM.Filter is null)
                CampgroundVM.Filter = new FilterVM(dataService);

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
