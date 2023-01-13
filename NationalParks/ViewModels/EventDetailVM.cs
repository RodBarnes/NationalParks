namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Event), "Event")]
public partial class EventDetailVM : DetailVM
{
    [ObservableProperty] Event npsEvent;
    [ObservableProperty] Dictionary<string, object> openMapDict;

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
    }
}
