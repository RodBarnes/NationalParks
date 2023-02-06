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
        StopsVM.HasContent = tour.HasStops;
        StopsVM.Items = tour.Stops.ToList<object>();
        TagsVM.HasContent = tour.HasTags;
        TagsVM.Items = tour.Tags.ToList<object>();
        TopicsVM.HasContent = tour.HasTopics;
        TopicsVM.Items = tour.Topics.ToList<object>();
        ActivitiesVM.HasContent = tour.HasActivities;
        ActivitiesVM.Items = tour.Activities.ToList<object>();
    }
}
