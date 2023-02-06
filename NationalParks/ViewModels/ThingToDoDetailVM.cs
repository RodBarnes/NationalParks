namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.ThingToDo), "Model")]
public partial class ThingToDoDetailVM : DetailVM
{
    [ObservableProperty] ThingToDo thingToDo;
    [ObservableProperty] RelatedParksVM relatedParksVM;
    [ObservableProperty] CollapsibleViewVM fullDescriptionVM;
    [ObservableProperty] CollapsibleViewVM organizationsVM;
    [ObservableProperty] CollapsibleListVM amenitiesVM;
    [ObservableProperty] CollapsibleListVM tagsVM;
    [ObservableProperty] CollapsibleListVM topicsVM;
    [ObservableProperty] CollapsibleListVM activitiesVM;

    public ThingToDoDetailVM(IMap map) : base(map)
    {
        Title = "Things To Do";
        RelatedParksVM = new RelatedParksVM("Related Parks", false);
        FullDescriptionVM = new CollapsibleViewVM("Full Description", false);
        OrganizationsVM = new CollapsibleViewVM("Related Organizations", false);
        AmenitiesVM = new CollapsibleListVM("Amenities", false);
        TagsVM = new CollapsibleListVM("Tags", false);
        TopicsVM = new CollapsibleListVM("Topics", false);
        ActivitiesVM = new CollapsibleListVM("Activities", false);
    }

    [RelayCommand]
    public void PopulateData()
    {
        RelatedParksVM.HasContent = ThingToDo.HasRelatedParks;
        RelatedParksVM.Items = ThingToDo.RelatedParks;
        AmenitiesVM.HasContent = ThingToDo.HasAmenities;
        AmenitiesVM.Items = ThingToDo.Amenities.ToList<object>();
        TagsVM.HasContent = ThingToDo.HasTags;
        TagsVM.Items = ThingToDo.Tags.ToList<object>();
        TopicsVM.HasContent = ThingToDo.HasTopics;
        TopicsVM.Items = ThingToDo.Topics.ToList<object>();
        ActivitiesVM.HasContent = ThingToDo.HasActivities;
        ActivitiesVM.Items = ThingToDo.Activities.ToList<object>();
    }
}
