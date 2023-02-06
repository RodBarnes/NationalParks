using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Place), "Model")]
public partial class PlaceDetailVM : DetailVM
{
    [ObservableProperty] Place place;
    [ObservableProperty] CollapsibleViewVM relatedParksVM;
    [ObservableProperty] CollapsibleViewVM bodyTextVM;
    [ObservableProperty] CollapsibleViewVM organizationsVM;
    [ObservableProperty] CollapsibleViewVM multimediaVM;
    [ObservableProperty] CollapsibleListVM tagsVM;
    [ObservableProperty] CollapsibleListVM quickFactsVM;
    [ObservableProperty] CollapsibleListVM amenitiesVM;

    public PlaceDetailVM(IMap map) : base(map)
    {
        Title = "Place";
        RelatedParksVM = new CollapsibleViewVM("Related Parks", false);
        BodyTextVM = new CollapsibleViewVM("Full Description", false);
        MultimediaVM = new CollapsibleViewVM("Multimedia", false);
        OrganizationsVM = new CollapsibleViewVM("Related Organizations", false);
        TagsVM = new CollapsibleListVM("Tags", false);
        QuickFactsVM = new CollapsibleListVM("Quick Facts", false);
        AmenitiesVM = new CollapsibleListVM("Amenities", false);
    }

    [RelayCommand]
    public void PopulateData()
    {
        TagsVM.HasContent = place.HasTags;
        TagsVM.Items = place.Tags.ToList<object>();
        QuickFactsVM.HasContent = place.HasQuickFacts;
        QuickFactsVM.Items = place.QuickFacts.ToList<object>();
        AmenitiesVM.HasContent = place.HasAmenities;
        AmenitiesVM.Items = place.Amenities.ToList<object>();
    }
}
