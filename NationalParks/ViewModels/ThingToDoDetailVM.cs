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
    [ObservableProperty] CollapsibleListVM topicsVM;
    [ObservableProperty] CollapsibleListVM activitiesVM;

    public ThingToDoDetailVM(IMap map) : base(map)
    {
        Title = "Things To Do";
        RelatedParksVM = new CollapsibleViewVM("Related Parks", false);
        FullDescriptionVM = new CollapsibleViewVM("Full Description", false);
        TagsVM = new CollapsibleViewVM("Tags", false);
        OrganizationsVM = new CollapsibleViewVM("Related Organizations", false);
        AmenitiesVM = new CollapsibleViewVM("Amenities", false);
        TopicsVM = new CollapsibleListVM("Topics", false);
        ActivitiesVM = new CollapsibleListVM("Activities", false);
    }

    [RelayCommand]
    public void PopulateData()
    {
        TopicsVM.HasContent = thingToDo.HasTopics;
        TopicsVM.Items = thingToDo.Topics.ToList<object>();
        ActivitiesVM.HasContent = thingToDo.HasActivities;
        ActivitiesVM.Items = thingToDo.Activities.ToList<object>();
    }
}
