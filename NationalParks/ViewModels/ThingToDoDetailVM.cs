namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.ThingToDo), "ThingToDo")]
public partial class ThingToDoDetailVM : DetailVM
{
    [ObservableProperty] ThingToDo thingToDo;

    [ObservableProperty]
    public Dictionary<string, object> openMapDict;

    [ObservableProperty] CollapsibleViewVM relatedParksVM;

    public ThingToDoDetailVM(IMap map) : base(map)
    {
        Title = "Things To Do";

        RelatedParksVM = new CollapsibleViewVM("Related Parks", false);
    }

    public void PopulateData()
    {
        OpenMapDict = new Dictionary<string, object>
        {
            { "Latitude", ThingToDo.DLatitude },
            { "Longitude", ThingToDo.DLongitude },
            { "Name", ThingToDo.Title }
        };
    }
}
