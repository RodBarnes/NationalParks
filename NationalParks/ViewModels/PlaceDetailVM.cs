using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Place), "Model")]
public partial class PlaceDetailVM : DetailVM
{
    [ObservableProperty] Place place;
    [ObservableProperty] AvatarVM avatarVM;
    [ObservableProperty] RelatedParksVM relatedParksVM;
    [ObservableProperty] CollapsibleViewVM bodyTextVM;
    [ObservableProperty] MultimediaVM multimediaVM;
    [ObservableProperty] CollapsibleListVM organizationsVM;
    [ObservableProperty] CollapsibleListVM tagsVM;
    [ObservableProperty] CollapsibleListVM quickFactsVM;
    [ObservableProperty] CollapsibleListVM amenitiesVM;

    public PlaceDetailVM(IMap map) : base(map)
    {
        Title = "Place";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Place;

        AvatarVM = new AvatarVM(Place.MainImage);

        BodyTextVM = new CollapsibleTextVM("Full Description", false, Place.BodyText);

        OrganizationsVM = new CollapsibleListVM("Related Organizations", false, Place.RelatedOrganizations.ToList<object>());
        TagsVM = new CollapsibleListVM("Tags", false, Place.Tags.ToList<object>());
        QuickFactsVM = new CollapsibleListVM("Quick Facts", false, Place.QuickFacts.ToList<object>());
        AmenitiesVM = new CollapsibleListVM("Amenities", false, Place.Amenities.ToList<object>());

        RelatedParksVM = new RelatedParksVM("Related Parks", false, Place.RelatedParks);
        MultimediaVM = new MultimediaVM("Multimedia", false, Place.Multimedia);
    }
}
