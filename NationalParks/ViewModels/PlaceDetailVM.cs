using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Place), "Model")]
public partial class PlaceDetailVM : DetailVM
{
    [ObservableProperty] Place place;
    [ObservableProperty] CollapsibleViewVM relatedParksVM;
    [ObservableProperty] CollapsibleViewVM bodyTextVM;
    [ObservableProperty] CollapsibleViewVM tagsVM;
    [ObservableProperty] CollapsibleViewVM organizationsVM;
    [ObservableProperty] CollapsibleViewVM quickFactsVM;
    [ObservableProperty] CollapsibleViewVM amenitiesVM;
    [ObservableProperty] CollapsibleViewVM multimediaVM;

    public PlaceDetailVM(IMap map) : base(map)
    {
        Title = "Place";
        RelatedParksVM = new CollapsibleViewVM("Related Parks", false);
        BodyTextVM = new CollapsibleViewVM("Full Description", false);
        TagsVM = new CollapsibleViewVM("Tags", false);
        OrganizationsVM = new CollapsibleViewVM("Related Organizations", false);
        QuickFactsVM = new CollapsibleViewVM("Quick Facts", false);
        AmenitiesVM = new CollapsibleViewVM("Amenities", false);
        MultimediaVM = new CollapsibleViewVM("Multimedia", false);
    }
}
