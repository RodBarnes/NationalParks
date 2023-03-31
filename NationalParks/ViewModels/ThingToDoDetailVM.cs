namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.ThingToDo), "Model")]
public partial class ThingToDoDetailVM : DetailVM
{
    [ObservableProperty] ThingToDo thingToDo;
    [ObservableProperty] RelatedParksVM relatedParks;
    [ObservableProperty] CollapsibleViewVM locationDescription;
    [ObservableProperty] CollapsibleViewVM fullDescription;
    [ObservableProperty] CollapsibleListVM organizations;
    [ObservableProperty] CollapsibleListVM amenities;
    [ObservableProperty] CollapsibleListVM tags;
    [ObservableProperty] CollapsibleListVM topics;
    [ObservableProperty] CollapsibleListVM activities;

    public ThingToDoDetailVM(IMap map) : base(map)
    {
        Title = "Things To Do";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = ThingToDo;

        LocationDescription = new CollapsibleTextVM("Directions", false, ThingToDo.LocationDescription);
        FullDescription = new CollapsibleTextVM("Full Description", false, ThingToDo.LongDescription);

        Organizations = new CollapsibleListVM("Related Organizations", false, ThingToDo.RelatedOrganizations.ToList<object>());
        Amenities = new CollapsibleListVM("Amenities", false, ThingToDo.Amenities.ToList<object>());
        Tags = new CollapsibleListVM("Tags", false, ThingToDo.Tags.ToList<object>());
        Topics = new CollapsibleListVM("Topics", false, ThingToDo.Topics.ToList<object>());
        Activities = new CollapsibleListVM("Activities", false, ThingToDo.Activities.ToList<object>());

        RelatedParks = new RelatedParksVM("Related Parks", false, ThingToDo.RelatedParks);
    }
}
