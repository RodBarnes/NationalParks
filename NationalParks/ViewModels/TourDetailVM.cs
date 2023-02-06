using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Tour), "Model")]
public partial class TourDetailVM : DetailVM
{
    [ObservableProperty] Tour tour;
    [ObservableProperty] CollapsibleViewVM tagsVM;
    [ObservableProperty] CollapsibleViewVM stopsVM;
    [ObservableProperty] CollapsibleListVM topicsVM;
    [ObservableProperty] CollapsibleListVM activitiesVM;

    public TourDetailVM(IMap map) : base(map)
    {
        Title = "Tour";
        TopicsVM = new CollapsibleListVM("Topics", false);
        ActivitiesVM = new CollapsibleListVM("Activities", false);
        TagsVM = new CollapsibleViewVM("Tags", false);
        StopsVM = new CollapsibleViewVM("Stops", false);
    }

    [RelayCommand]
    public void PopulateData()
    {
        TopicsVM.HasContent = tour.HasTopics;
        TopicsVM.Items = tour.Topics.ToList<object>();
        ActivitiesVM.HasContent = tour.HasActivities;
        ActivitiesVM.Items = tour.Activities.ToList<object>();
    }
}
