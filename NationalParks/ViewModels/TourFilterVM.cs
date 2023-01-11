namespace NationalParks.ViewModels;

[QueryProperty(nameof(TourVM), "VM")]
public partial class TourFilterVM : BaseVM
{
    // Query properties
    public TourListVM TourVM { get; set; }

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
        if (TourVM.Filter is null)
            TourVM.Filter = new FilterVM(true);

        // Populate the selected items
        foreach (var topic in TourVM.Filter.Topics)
        {
            SelectedTopics.Add(topic);
        }
        foreach (var activity in TourVM.Filter.Activities)
        {
            SelectedActivities.Add(activity);
        }
        foreach (var state in TourVM.Filter.States)
        {
            SelectedStates.Add(state);
        }
    }

    [RelayCommand]
    async Task ApplyFilter()
    {
        // Update the filter
        TourVM.Filter.Topics.Clear();
        foreach (var o in SelectedTopics)
        {
            if (o is Models.Topic topic)
            {
                TourVM.Filter.Topics.Add(topic);
            }
        }
        TourVM.Filter.Activities.Clear();
        foreach (var o in SelectedActivities)
        {
            if (o is Models.Activity activity)
            {
                TourVM.Filter.Activities.Add(activity);
            }
        }
        TourVM.Filter.States.Clear();
        foreach (var o in SelectedStates)
        {
            if (o is Models.State state)
            {
                TourVM.Filter.States.Add(state);
            }
        }

        // Clear the list
        TourVM.ClearData();

        await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
        {
            {"Filter", TourVM.Filter }
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
        TourVM.Filter.Topics.Clear();
        TourVM.Filter.Activities.Clear();
        TourVM.Filter.States.Clear();

        // Clear the list
        TourVM.ClearData();

        Shell.Current.DisplayAlert("Filter", "All filter values have been cleared.", "OK");
    }

}
