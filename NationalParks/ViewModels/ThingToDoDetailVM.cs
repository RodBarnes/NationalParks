namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.ThingToDo), "ThingToDo")]
public partial class ThingToDoDetailVM : DetailVM
{
    [ObservableProperty] ThingToDo thingToDo;
    [ObservableProperty] CollapsibleViewVM relatedParksVM;

    public ThingToDoDetailVM(IMap map) : base(map)
    {
        Title = "Things To Do";

        RelatedParksVM = new CollapsibleViewVM("Related Parks", false);
    }
}
