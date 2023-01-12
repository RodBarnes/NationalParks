namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Event), "Event")]
public partial class EventDetailVM : DetailVM
{
    [ObservableProperty] Event npsEvent;

    [ObservableProperty]
    public Dictionary<string, object> openMapDict;

    [ObservableProperty]
    public Dictionary<string, object> goToImagesDict;

    public EventDetailVM(IMap map) : base(map)
    {
        Title = "Events";
    }

    public void PopulateData()
    {
        OpenMapDict = new Dictionary<string, object>
        {
            { "Latitude", NpsEvent.DLatitude },
            { "Longitude", NpsEvent.DLongitude },
            { "Name", NpsEvent.Title }
        };

        GoToImagesDict = new Dictionary<string, object>
        {
            { "PageName", nameof(EventImageListPage) },
            { "ParamName", "Images" },
            { "Object", NpsEvent.Images }
        };
    }
}
