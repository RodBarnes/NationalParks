namespace NationalParks.Models;

public class Place
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string ListingDescription { get; set; }
    public List<Image> Images { get; set; }
    public List<Park> RelatedParks { get; set; }
    public List<object> RelatedOrganizations { get; set; }
    public List<string> Tags { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string LatLong { get; set; }
    public string BodyText { get; set; }
    public string AudioDescription { get; set; }
    public string IsPassportStampLocation { get; set; }
    public string PassportStampLocationDescription { get; set; }
    public List<Image> PassportStampImages { get; set; }
    public string ManagedByUrl { get; set; }
    public string IsOpenToPublic { get; set; }
    public string IsMapPinHidden { get; set; }
    public string NpmapId { get; set; }
    public string GeometryPoiId { get; set; }
    public string IsManagedByNps { get; set; }
    public List<string> Amenities { get; set; }
    public string ManagedByOrg { get; set; }
    public List<QuickFact> QuickFacts { get; set; }
    public string Location { get; set; }
    public string LocationDescription { get; set; }
    public string Credit { get; set; }
    public List<object> Multimedia { get; set; }

    // Derived properties
    public ImageSource MainImage
    {
        get
        {
            if (Images.Count > 0)
                return ImageSource.FromUri(new Uri(Images.FirstOrDefault().Url));
            else
                return ImageSource.FromFile("no_image_green.png");
        }
    }
    public double DLatitude
    {
        get
        {
            if (double.TryParse(Latitude, out double d))
            {
                return d;
            }
            else
            {
                return -1;
            }
        }
    }
    public double DLongitude
    {
        get
        {
            if (double.TryParse(Longitude, out double d))
            {
                return d;
            }
            else
            {
                return -1;
            }
        }
    }
    public bool HasUrl { get => !String.IsNullOrEmpty(Url); }
}
