using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Place), "Place")]
public partial class PlaceDetailVM : DetailVM
{
    [ObservableProperty] Place place;

    [ObservableProperty]
    public Dictionary<string, object> openMapDict;

    [ObservableProperty]
    public Dictionary<string, object> goToImagesDict;

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

    public void PopulateData()
    {
        OpenMapDict = new Dictionary<string, object>
        {
            { "Latitude", Place.DLatitude },
            { "Longitude", Place.DLongitude },
            { "Name", Place.Title }
        };

        GoToImagesDict = new Dictionary<string, object>
        {
            { "PageName", nameof(PlaceImageListPage) },
            { "ParamName", "Images" },
            { "Object", Place.Images }
        };
    }
}
