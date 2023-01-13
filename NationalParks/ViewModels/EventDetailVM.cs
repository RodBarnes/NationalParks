namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Event), "Model")]
public partial class EventDetailVM : DetailVM
{
    [ObservableProperty] Event npsEvent;

    public EventDetailVM(IMap map) : base(map)
    {
        Title = "Events";
    }
}
