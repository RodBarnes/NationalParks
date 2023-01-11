using NationalParks.Services;

namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(PlaceVM), "VM")]
    public partial class PlaceFilterVM : BaseVM
    {
        // Query properties
        public PlaceListVM PlaceVM { get; set; }

        // Selected values
        public ObservableCollection<object> SelectedStates { get; set; } = new();

        public PlaceFilterVM()
        {
            Title = "Filter";
        }

        public void PopulateData()
        {
            if (PlaceVM.Filter is null)
                PlaceVM.Filter = new FilterVM(true);

            // Populate the selected items
            foreach (var state in PlaceVM.Filter.States)
            {
                SelectedStates.Add(state);
            }
        }

        [RelayCommand]
        async Task ApplyFilter()
        {
            // Update the filter
            PlaceVM.Filter.States.Clear();
            foreach (var o in SelectedStates)
            {
                if (o is Models.State state)
                {
                    PlaceVM.Filter.States.Add(state);
                }
            }

            // Clear the list
            PlaceVM.ClearData();

            await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
            {
                {"Filter", PlaceVM.Filter }
            });
        }

        [RelayCommand]
        public void ClearFilter()
        {
            // Clear the selections
            SelectedStates.Clear();

            // Clear the filter
            PlaceVM.Filter.States.Clear();

            // Clear the list
            PlaceVM.ClearData();

            Shell.Current.DisplayAlert("Filter", "All filter values have been cleared.", "OK");
        }
    }
}
