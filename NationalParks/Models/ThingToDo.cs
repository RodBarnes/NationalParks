namespace NationalParks.Models;

public class ThingToDo : BaseModel
{
    public new string Description { get => ShortDescription; }
    public string ShortDescription { get; set; }
    public List<RelatedPark> RelatedParks { get; set; }
    public List<object> RelatedOrganizations { get; set; }
    public List<string> Tags { get; set; }
    public string GeometryPoiId { get; set; }
    public List<object> Amenities { get; set; }
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
    public List<Activity> Activities { get; set; }
    public string ActivityDescription { get; set; }
    public string LocationDescription { get; set; }
    public string FoFeesApply { get; set; }
    public string LongDescription { get; set; }
    public string ReservationDescription { get; set; }
    public List<string> Season { get; set; }
    public List<Topic> Topics { get; set; }
    public string DurationDescription { get; set; }
    public string ArePetsPermitted { get; set; }
    public List<string> TimeOfDay { get; set; }
    public string Duration { get; set; }
    public string Credit { get; set; }

    // Derived properties
    public bool HasRelatedParks => (RelatedParks is not null) && RelatedParks.Count > 0;
}
