namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.ThingToDo), "Model")]
public partial class ThingToDoDetailVM : DetailVM
{
    [ObservableProperty] ThingToDo thingToDo;
    [ObservableProperty] CollapsibleViewVM relatedParksVM;
    [ObservableProperty] CollapsibleViewVM fullDescriptionVM;
    [ObservableProperty] CollapsibleViewVM tagsVM;
    [ObservableProperty] CollapsibleViewVM organizationsVM;
    [ObservableProperty] CollapsibleViewVM amenitiesVM;
    [ObservableProperty] CollapsibleViewVM topicsVM;
    [ObservableProperty] CollapsibleViewVM activitiesVM;

    public ThingToDoDetailVM(IMap map) : base(map)
    {
        Title = "Things To Do";
        RelatedParksVM = new CollapsibleViewVM("Related Parks", false);
        FullDescriptionVM = new CollapsibleViewVM("Full Description", false);
        TagsVM = new CollapsibleViewVM("Tags", false);
        OrganizationsVM = new CollapsibleViewVM("Related Organizations", false);
        AmenitiesVM = new CollapsibleViewVM("Amenities", false);
        TopicsVM = new CollapsibleViewVM("Topics", false);
        ActivitiesVM = new CollapsibleViewVM("Activities", false);
    }
}
