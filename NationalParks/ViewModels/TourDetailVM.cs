using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Tour), "Tour")]
public partial class TourDetailVM : DetailVM
{
    [ObservableProperty] Tour tour;
    [ObservableProperty] CollapsibleViewVM tagsVM;
    [ObservableProperty] CollapsibleViewVM stopsVM;
    [ObservableProperty] CollapsibleViewVM topicsVM;
    [ObservableProperty] CollapsibleViewVM activitiesVM;

    public TourDetailVM(IMap map) : base(map)
    {
        Title = "Tour";

        TopicsVM = new CollapsibleViewVM("Topics", false);
        ActivitiesVM = new CollapsibleViewVM("Activities", false);
        TagsVM = new CollapsibleViewVM("Tags", false);
        StopsVM = new CollapsibleViewVM("Stops", false);
    }
}
