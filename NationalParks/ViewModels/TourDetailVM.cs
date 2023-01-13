using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Tour), "Tour")]
public partial class TourDetailVM : DetailVM
{
    [ObservableProperty] Tour tour;
    [ObservableProperty] Dictionary<string, object> openMapDict;
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

    public void PopulateData()
    {
        OpenMapDict = new Dictionary<string, object>
        {
            { "Latitude", Tour.DLatitude },
            { "Longitude", Tour.DLongitude },
            { "Name", Tour.Title }
        };
    }
}
