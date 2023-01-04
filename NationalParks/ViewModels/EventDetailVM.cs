namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Event), "Event")]
public partial class EventDetailVM : BaseVM
{
    IMap map;

    [ObservableProperty]
    Event eventx;

    public EventDetailVM(IMap map)
    {
        Title = "Events";
        this.map = map;
    }
}
