using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Place), "Model")]
public partial class PlaceDetailVM : DetailVM
{
    [ObservableProperty] Place place;
    [ObservableProperty] RelatedParksVM relatedParksVM;
    [ObservableProperty] CollapsibleViewVM bodyTextVM;
    [ObservableProperty] CollapsibleViewVM multimediaVM;
    [ObservableProperty] CollapsibleListVM organizationsVM;
    [ObservableProperty] CollapsibleListVM tagsVM;
    [ObservableProperty] CollapsibleListVM quickFactsVM;
    [ObservableProperty] CollapsibleListVM amenitiesVM;

    public PlaceDetailVM(IMap map) : base(map)
    {
        Title = "Place";
        BodyTextVM = new CollapsibleViewVM("Full Description", false);
        MultimediaVM = new CollapsibleViewVM("Multimedia", false);
    }

    [RelayCommand]
    public void PopulateData()
    {
        RelatedParksVM = new RelatedParksVM("Related Parks", false, Place.RelatedParks);
        OrganizationsVM = new CollapsibleListVM("Related Organizations", false, Place.RelatedOrganizations.ToList<object>());
        TagsVM = new CollapsibleListVM("Tags", false, Place.Tags.ToList<object>());
        QuickFactsVM = new CollapsibleListVM("Quick Facts", false, Place.QuickFacts.ToList<object>());
        AmenitiesVM = new CollapsibleListVM("Amenities", false, Place.Amenities.ToList<object>());
    }
}
