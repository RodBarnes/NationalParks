namespace NationalParks.Models;

public class Place : MainModel
{
    public string Title { get; set; }
    public string ListingDescription { get; set; }
    public List<RelatedPark> RelatedParks { get; set; }
    public List<Organization> RelatedOrganizations { get; set; }
    public List<string> Tags { get; set; }
    public string LatLong { get; set; }
    public string BodyText { get; set; }
    public string AudioDescription { get; set; }
    public int IsPassportStampLocation { get; set; }
    public string PassportStampLocationDescription { get; set; }
    public List<Image> PassportStampImages { get; set; }
    public string ManagedByUrl { get; set; }
    public int IsOpenToPublic { get; set; }
    public int IsMapPinHidden { get; set; }
    public string NpmapId { get; set; }
    public string GeometryPoiId { get; set; }
    public int IsManagedByNps { get; set; }
    public List<string> Amenities { get; set; }
    public string ManagedByOrg { get; set; }
    public List<QuickFact> QuickFacts { get; set; }
    public string Location { get; set; }
    public string LocationDescription { get; set; }
    public string Credit { get; set; }
    public List<Multimedia> Multimedia { get; set; }

    // Derived properties
    public string ManagedBy => IsManagedByNps == 1 ? "National Park Service" : ManagedByOrg;
    public bool HasBodyText => !String.IsNullOrEmpty(BodyText);
    public bool HasTags => (Tags is not null) && Tags.Count > 0;
    public bool HasRelatedOrganizations => (RelatedOrganizations is not null) && RelatedOrganizations.Count > 0;
    public bool HasRelatedParks => (RelatedParks is not null) && RelatedParks.Count > 0;
    public bool HasQuickFacts => (QuickFacts is not null) && QuickFacts.Count > 0;
    public bool HasAmenities => (Amenities is not null) && Amenities.Count > 0;
    public bool HasMultiMedia => (Multimedia is not null) && Multimedia.Count > 0;
}
