using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Tour), "Model")]
public partial class TourDetailVM : DetailVM
{
    [ObservableProperty] Tour tour;
    [ObservableProperty] CollapsibleListVM stops;
    [ObservableProperty] CollapsibleListVM tags;
    [ObservableProperty] CollapsibleListVM topics;
    [ObservableProperty] CollapsibleListVM activities;

    public TourDetailVM(IMap map) : base(map)
    {
        Title = "Tour";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Tour;

        Stops = new CollapsibleListVM("Stops", false, Tour.Stops.ToList<object>());
        Tags = new CollapsibleListVM("Tags", false, Tour.Tags.ToList<object>());
        Topics = new CollapsibleListVM("Topics", false, Tour.Topics.ToList<object>());
        Activities = new CollapsibleListVM("Activities", false, Tour.Activities.ToList<object>());
    }
}
