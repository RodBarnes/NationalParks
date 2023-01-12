namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.ThingToDo), "ThingToDo")]
public partial class ThingToDoDetailVM : DetailVM
{
    [ObservableProperty] ThingToDo thingToDo;

    [ObservableProperty]
    public Dictionary<string, object> openMapDict;

    [ObservableProperty]
    public Dictionary<string, object> goToImagesDict;

    public ThingToDoDetailVM(IMap map) : base(map)
    {
        Title = "Things To Do";
    }

    public void PopulateData()
    {
        OpenMapDict = new Dictionary<string, object>
        {
            { "Latitude", ThingToDo.DLatitude },
            { "Longitude", ThingToDo.DLongitude },
            { "Name", ThingToDo.Title }
        };

        GoToImagesDict = new Dictionary<string, object>
        {
            { "PageName", nameof(ThingToDoImageListPage) },
            { "ParamName", "Images" },
            { "Object", ThingToDo.Images }
        };
    }
}
