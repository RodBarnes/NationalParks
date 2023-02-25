namespace NationalParks.Models;

public partial class Multimedia : BaseModel
{
    public string PermalinkUrl { get; set; }
    public Splashimage SplashImage { get; set; }
    public List<RelatedPark> RelatedParks { get; set; }
    public List<string> Tags { get; set; }
    public string AudioDescription { get; set; }
    public string AudioDescriptionUrl { get; set; }
    public string GeometryPoiId { get; set; }
    public int? DurationMs { get; set; }
    public string Credit { get; set; }
    public string Transcript { get; set; }
    public string CallToAction { get; set; }
    public string CallToActionUrl { get; set; }
    public bool AudioDescribedBuiltIn { get; set; }
    public bool HasOpenCaptions { get; set; }
    public bool IsBRoll { get; set; }
    public List<Captionfile> CaptionFiles { get; set; }
    public List<Specifications> Versions { get; set; }

    #region Derived Properties

    public new ImageSource MainImage => GetMainImageFromListingImage();

    #endregion

    private ImageSource GetMainImageFromListingImage()
    {
        ImageSource source = null;

        if (!String.IsNullOrEmpty(SplashImage.Url))
        {
            source = ImageSource.FromUri(new Uri(SplashImage.Url));
        }
        else
        {
            source = ImageSource.FromFile("nps");
        }

        return source;
    }
}

public class Splashimage
{
    public string Url { get; set; }
}

public class Captionfile
{
    public string Language { get; set; }
    public string FileType { get; set; }
    public string Url { get; set; }
}

public class Specifications
{
    public float? FileSizeKb { get; set; }
    public string FileType { get; set; }
    public float AspectRatio { get; set; }
    public int HeightPixels { get; set; }
    public string Url { get; set; }
    public int WidthPixels { get; set; }
}
