namespace NationalParks.ViewModels;

[QueryProperty(nameof(ListVM), "VM")]
public partial class TourFilterVM : BaseVM
{
    // Query properties
    public TourListVM ListVM { get; set; }

    // Selected values
    public ObservableCollection<object> SelectedTopics { get; set; } = new();
    public ObservableCollection<object> SelectedActivities { get; set; } = new();
    public ObservableCollection<object> SelectedStates { get; set; } = new();

    public TourFilterVM()
    {
        Title = "Filter";
    }

    public void PopulateData()
    {
        if (ListVM.Filter is null)
            ListVM.Filter = new FilterVM(true);

        // Populate the selected items
        foreach (var topic in ListVM.Filter.Topics)
        {
            SelectedTopics.Add(topic);
        }
        foreach (var activity in ListVM.Filter.Activities)
        {
            SelectedActivities.Add(activity);
        }
        foreach (var state in ListVM.Filter.States)
        {
            SelectedStates.Add(state);
        }
    }

    [RelayCommand]
    async Task ApplyFilter()
    {
        // Update the filter
        ListVM.Filter.Topics.Clear();
        foreach (var o in SelectedTopics)
        {
            if (o is Models.Topic topic)
            {
                ListVM.Filter.Topics.Add(topic);
            }
        }
        ListVM.Filter.Activities.Clear();
        foreach (var o in SelectedActivities)
        {
            if (o is Models.Activity activity)
            {
                ListVM.Filter.Activities.Add(activity);
            }
        }
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
        SelectedTopics.Clear();
        SelectedActivities.Clear();
        SelectedStates.Clear();

        // Clear the filter
        ListVM.Filter.Topics.Clear();
        ListVM.Filter.Activities.Clear();
        ListVM.Filter.States.Clear();

        // Clear the list
        ListVM.ClearData();

        Shell.Current.DisplayAlert("Filter", "All filter values have been cleared.", "OK");
    }

}
