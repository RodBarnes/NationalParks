namespace NationalParks.Models;

public class ThingToDo : BaseModel
{
    public string ShortDescription { get; set; }
    public ICollection<RelatedPark> RelatedParks { get; set; }
    public ICollection<Organization> RelatedOrganizations { get; set; }
    public ICollection<string> Tags { get; set; }
    public string GeometryPoiId { get; set; }
    public ICollection<object> Amenities { get; set; }
    public string Location { get; set; }
    public string SeasonDescription { get; set; }
    public string AccessibilityInformation { get; set; }
    public string IsReservationRequired { get; set; }
    public string AgeDescription { get; set; }
    public string PetsDescription { get; set; }
    public string TimeOfDayDescription { get; set; }
    public string FeeDescription { get; set; }
    public string Age { get; set; }
    public string ArePetsPermittedWithRestrictions { get; set; }
    public ICollection<Activity> Activities { get; set; }
    public string ActivityDescription { get; set; }
    public string LocationDescription { get; set; }
    public string DoFeesApply { get; set; }
    public string LongDescription { get; set; }
    public string ReservationDescription { get; set; }
    public ICollection<string> Season { get; set; }
    public ICollection<Topic> Topics { get; set; }
    public string DurationDescription { get; set; }
    public string ArePetsPermitted { get; set; }
    public ICollection<string> TimeOfDay { get; set; }
    public string Duration { get; set; }
    public string Credit { get; set; }

    #region Derived Properties

    public new string Description { get => ShortDescription; }
    public bool HasLongDescription => !String.IsNullOrEmpty(LongDescription);
    public bool HasRelatedParks => (RelatedParks is not null) && RelatedParks.Count > 0;
    public bool HasRelatedOrganizations => (RelatedOrganizations is not null) && RelatedOrganizations.Count > 0;
    public bool HasTags => (Tags is not null) && Tags.Count > 0;
    public bool HasAmenities => (Amenities is not null) && Amenities.Count > 0;
    public bool HasTopics => (Topics is not null) && Topics.Count > 0;
    public bool HasActivities => (Activities is not null) && Activities.Count > 0;

    #endregion
}
