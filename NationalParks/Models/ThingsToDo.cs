namespace NationalParks.Models;

public class ThingsToDo
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public Image[] Images { get; set; }
    public RelatedPark[] RelatedParks { get; set; }
    public object[] RelatedOrganizations { get; set; }
    public string[] Tags { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string GeometryPoiId { get; set; }
    public object[] Amenities { get; set; }
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
    public System.Diagnostics.Activity[] Activities { get; set; }
    public string ActivityDescription { get; set; }
    public string LocationDescription { get; set; }
    public string FoFeesApply { get; set; }
    public string LongDescription { get; set; }
    public string ReservationDescription { get; set; }
    public string[] Season { get; set; }
    public Topic[] Topics { get; set; }
    public string DurationDescription { get; set; }
    public string ArePetsPermitted { get; set; }
    public string[] TimeOfDay { get; set; }
    public string Duration { get; set; }
    public string Credit { get; set; }
}
