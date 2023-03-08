namespace NationalParks.Models;

public partial class Person : BaseModel
{
    public string ListingDescription
    {
        get { return Description; }
        set { Description = value; }
    }
    public List<RelatedPark> RelatedParks { get; set; }
    public List<Organization> RelatedOrganizations { get; set; }
    public List<string> Tags { get; set; }
    public string LatLong { get; set; }
    public string BodyText { get; set; }
    public string GeometryPoiId { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public List<QuickFact> QuickFacts { get; set; }
    public string Credit { get; set; }

    #region Derived Properties

    public string FullName { get => $"{FirstName} {MiddleName} {LastName}"; }
    public bool HasTags => (Tags is not null) && Tags.Count > 0;
    public bool HasQuickFacts => (QuickFacts is not null) && QuickFacts.Count > 0;
    public bool HasRelatedOrganizations => (RelatedOrganizations is not null) && RelatedOrganizations.Count > 0;
    public bool HasRelatedParks => (RelatedParks is not null) && RelatedParks.Count > 0;
    public bool HasBodyText => !String.IsNullOrEmpty(BodyText);

    #endregion
}
