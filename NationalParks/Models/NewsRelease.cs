namespace NationalParks.Models;

public partial class NewsRelease : BaseModel
{
    public string ParkCode { get; set; }
    public string Abstract { get; set; }
    public Image Image { get; set; }
    public List<RelatedPark> RelatedParks { get; set; }
    public List<Organization> RelatedOrganizations { get; set; }
    public string GeometryPoiId { get; set; }
    public string ReleaseDate { get; set; }
    public string Credit { get; set; }
    public string LastIndexedDate { get; set; }

    #region Derived Properties

    public new string Description { get => Abstract; }

    public new ImageSource MainImage => (!String.IsNullOrEmpty(Image?.Url)) ? ImageSource.FromUri(new Uri(Image.Url)) : ImageSource.FromFile("nps");

    #endregion
}
