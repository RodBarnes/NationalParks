using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Tour), "Tour")]
public partial class TourDetailVM : DetailVM
{
    [ObservableProperty] Tour tour;

    [ObservableProperty]
    public Dictionary<string, object> openMapDict;

    [ObservableProperty]
    public Dictionary<string, object> goToImagesDict;

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

        GoToImagesDict = new Dictionary<string, object>
        {
            { "PageName", nameof(TourImageListPage) },
            { "ParamName", "Images" },
            { "Object", Tour.Images }
        };
    }
}
