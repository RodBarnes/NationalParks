namespace NationalParks.Models;

public class Place
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string listingDescription { get; set; }
    public Image[] Images { get; set; }
    public RelatedPark[] RelatedParks { get; set; }
    public object[] RelatedOrganizations { get; set; }
    public string[] Tags { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string LatLong { get; set; }
    public string BodyText { get; set; }
    public string AudioDescription { get; set; }
    public string IsPassportStampLocation { get; set; }
    public string PassportStampLocationDescription { get; set; }
    public object[] PassportStampImages { get; set; }
    public string ManagedByUrl { get; set; }
    public string IsOpenToPublic { get; set; }
    public string IsMapPinHidden { get; set; }
    public string NpmapId { get; set; }
    public string GeometryPoiId { get; set; }
    public string IsManagedByNps { get; set; }
    public object[] Amenities { get; set; }
    public string ManagedByOrg { get; set; }
    public object[] QuickFacts { get; set; }
    public string Location { get; set; }
    public string LocationDescription { get; set; }
    public string Credit { get; set; }
    public object[] Multimedia { get; set; }
}
