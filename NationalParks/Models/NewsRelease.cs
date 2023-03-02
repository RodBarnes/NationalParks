namespace NationalParks.Models;

public partial class NewsRelease : BaseModel
{
    public string ParkCode { get; set; }
    public string Abstract
    {
        get { return Description; }
        set { Description = value; }
    }
    public Image Image { get; set; }
    public ICollection<RelatedPark> RelatedParks { get; set; }
    public ICollection<Organization> RelatedOrganizations { get; set; }
    public string GeometryPoiId { get; set; }
    public string ReleaseDate { get; set; }
    public string Credit { get; set; }
    public string LastIndexedDate { get; set; }

    #region Derived Properties

    public bool HasCredit { get => !String.IsNullOrEmpty(Credit); }
    public new ImageSource MainImage => (!String.IsNullOrEmpty(Image?.Url)) ? ImageSource.FromUri(new Uri(Image.Url)) : ImageSource.FromFile("nps");

    #endregion
}
