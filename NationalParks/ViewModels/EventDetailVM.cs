namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Event), "Event")]
public partial class EventDetailVM : DetailVM
{
    [ObservableProperty] Event npsEvent;

    public EventDetailVM(IMap map) : base(map)
    {
        Title = "Events";
    }

    public void PopulateData()
    {
        Model = NpsEvent;
    }
}
