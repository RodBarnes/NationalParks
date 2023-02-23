namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.ThingToDo), "Model")]
public partial class ThingToDoDetailVM : DetailVM
{
    [ObservableProperty] ThingToDo thingToDo;
    [ObservableProperty] AvatarVM avatarVM;
    [ObservableProperty] RelatedParksVM relatedParksVM;
    [ObservableProperty] CollapsibleViewVM fullDescriptionVM;
    [ObservableProperty] CollapsibleListVM organizationsVM;
    [ObservableProperty] CollapsibleListVM amenitiesVM;
    [ObservableProperty] CollapsibleListVM tagsVM;
    [ObservableProperty] CollapsibleListVM topicsVM;
    [ObservableProperty] CollapsibleListVM activitiesVM;

    public ThingToDoDetailVM(IMap map) : base(map)
    {
        Title = "Things To Do";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = ThingToDo;

        AvatarVM = new AvatarVM(ThingToDo.MainImage);

        FullDescriptionVM = new CollapsibleTextVM("Full Description", false, ThingToDo.LongDescription);

        OrganizationsVM = new CollapsibleListVM("Related Organizations", false, ThingToDo.RelatedOrganizations.ToList<object>());
        AmenitiesVM = new CollapsibleListVM("Amenities", false, ThingToDo.Amenities.ToList<object>());
        TagsVM = new CollapsibleListVM("Tags", false, ThingToDo.Tags.ToList<object>());
        TopicsVM = new CollapsibleListVM("Topics", false, ThingToDo.Topics.ToList<object>());
        ActivitiesVM = new CollapsibleListVM("Activities", false, ThingToDo.Activities.ToList<object>());

        RelatedParksVM = new RelatedParksVM("Related Parks", false, ThingToDo.RelatedParks);
    }
}
