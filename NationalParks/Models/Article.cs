namespace NationalParks.Models;

public class Article : BaseModel
{
    public string GeometryPoiId { get; set; }
    public string ListingDescription { get; set; }
    public List<Image> ListingImage { get; set; }
    public List<RelatedPark> RelatedParks { get; set; }
    public string LatLong { get; set; }
}
