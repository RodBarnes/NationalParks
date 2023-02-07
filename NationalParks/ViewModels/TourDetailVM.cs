using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Tour), "Model")]
public partial class TourDetailVM : DetailVM
{
    [ObservableProperty] Tour tour;
    [ObservableProperty] CollapsibleListVM stopsVM;
    [ObservableProperty] CollapsibleListVM tagsVM;
    [ObservableProperty] CollapsibleListVM topicsVM;
    [ObservableProperty] CollapsibleListVM activitiesVM;

    public TourDetailVM(IMap map) : base(map)
    {
        Title = "Tour";
        StopsVM = new CollapsibleListVM("Stops", false);
        TagsVM = new CollapsibleListVM("Tags", false);
        TopicsVM = new CollapsibleListVM("Topics", false);
        ActivitiesVM = new CollapsibleListVM("Activities", false);
    }

    [RelayCommand]
    public void PopulateData()
    {
        StopsVM.HasContent = Tour.HasStops;
        StopsVM.Items = Tour.Stops.ToList<object>();
        TagsVM.HasContent = Tour.HasTags;
        TagsVM.Items = Tour.Tags.ToList<object>();
        TopicsVM.HasContent = Tour.HasTopics;
        TopicsVM.Items = Tour.Topics.ToList<object>();
        ActivitiesVM.HasContent = Tour.HasActivities;
        ActivitiesVM.Items = Tour.Activities.ToList<object>();
    }
}
