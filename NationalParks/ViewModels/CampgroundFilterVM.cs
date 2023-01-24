namespace NationalParks.ViewModels;

[QueryProperty(nameof(ListVM), "VM")]
public partial class CampgroundFilterVM : BaseVM
{
    // Query properties
    public CampgroundListVM ListVM { get; set; }

    // Selected values
    public ObservableCollection<object> SelectedStates { get; set; } = new();

    public CampgroundFilterVM()
    {
        Title = "Filter";
    }

    public void PopulateData()
    {
        if (ListVM.Filter is null)
            ListVM.Filter = new FilterVM(true);

        // Populate the selected items
        foreach (var state in ListVM.Filter.States)
        {
            SelectedStates.Add(state);
        }
    }

    [RelayCommand]
    async Task ApplyFilter()
    {
        // Update the filter
        ListVM.Filter.States.Clear();
        foreach (var o in SelectedStates)
        {
            if (o is Models.State state)
            {
                ListVM.Filter.States.Add(state);
            }
        }

        // Clear the list
        ListVM.ClearData();

        await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
        {
            {"Filter", ListVM.Filter }
        });
    }

    [RelayCommand]
    public void ClearFilter()
    {
        // Clear the selections
        SelectedStates.Clear();

        // Clear the filter
        ListVM.Filter.States.Clear();

        // Clear the list
        ListVM.ClearData();

        Shell.Current.DisplayAlert("Filter", "All filter values have been cleared.", "OK");
    }
}
