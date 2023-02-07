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
        RelatedParksVM = new RelatedParksVM("Related Parks", false);
        BodyTextVM = new CollapsibleViewVM("Full Description", false);
        MultimediaVM = new CollapsibleViewVM("Multimedia", false);
        OrganizationsVM = new CollapsibleListVM("Related Organizations", false);
        TagsVM = new CollapsibleListVM("Tags", false);
        QuickFactsVM = new CollapsibleListVM("Quick Facts", false);
        AmenitiesVM = new CollapsibleListVM("Amenities", false);
    }

    [RelayCommand]
    public void PopulateData()
    {
        RelatedParksVM.HasContent = Place.HasRelatedParks;
        RelatedParksVM.Items = Place.RelatedParks;
        OrganizationsVM.HasContent = Place.HasRelatedOrganizations;
        OrganizationsVM.Items = Place.RelatedOrganizations.ToList<object>();
        TagsVM.HasContent = Place.HasTags;
        TagsVM.Items = Place.Tags.ToList<object>();
        QuickFactsVM.HasContent = Place.HasQuickFacts;
        QuickFactsVM.Items = Place.QuickFacts.ToList<object>();
        AmenitiesVM.HasContent = Place.HasAmenities;
        AmenitiesVM.Items = Place.Amenities.ToList<object>();
    }
}
