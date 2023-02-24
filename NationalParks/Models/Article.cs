namespace NationalParks.Models;

public class Article : BaseModel
{
    public string GeometryPoiId { get; set; }
    public string ListingDescription { get; set; }
    public Image ListingImage { get; set; }
    public List<RelatedPark> RelatedParks { get; set; }
    public string LatLong { get; set; }

    #region Derived Properties

    public new ImageSource MainImage => GetMainImageFromListingImage();

    #endregion

    private ImageSource GetMainImageFromListingImage()
    {
        ImageSource source = null;

        if (!String.IsNullOrEmpty(ListingImage.Url))
        {
            source = ImageSource.FromUri(new Uri(ListingImage.Url));
        }
        else
        {
            source = ImageSource.FromFile("nps");
        }

        return source;
    }
}
