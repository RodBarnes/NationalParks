namespace NationalParks.Models;

public class Campground : BaseModel
{
    public string ParkCode { get; set; }
    public string AudioDescription { get; set; }
    public string IsPassportStampLocation { get; set; }
    public string PassportStampLocationDescription { get; set; }
    public List<object> PassportStampImages { get; set; }
    public string GeometryPoiId { get; set; }
    public string ReservationInfo { get; set; }
    public string ReservationUrl { get; set; }
    public string RegulationsUrl { get; set; }
    public string RegulationsOverview { get; set; }
    public Amenities Amenities { get; set; }
    public Contacts Contacts { get; set; }
    public List<Fee> Fees { get; set; }
    public string DirectionsOverview { get; set; }
    public string DirectionsUrl { get; set; }
    public List<OperatingHours> OperatingHours { get; set; }
    public List<Address> Addresses { get; set; }
    public Address PhysicalAddress { get => Addresses.Where(a => a.Type == "Physical").FirstOrDefault(); }
    public string WeatherOverview { get; set; }
    public string NumberOfSitesReservable { get; set; }
    public string NumberOfSitesFirstComeFirstServe { get; set; }
    public Campsites Campsites { get; set; }
    public Accessibility Accessibility { get; set; }
    public List<RelatedMultimedia> Multimedia { get; set; }
    public string LastIndexDate { get; set; }

    #region Derived Properties

    public bool HasReservationUrl => !String.IsNullOrEmpty(ReservationUrl);
    public bool HasRegulationsUrl => !String.IsNullOrEmpty(RegulationsUrl);
    public bool HasDirections => !String.IsNullOrEmpty(DirectionsOverview) || (PhysicalAddress is not null);
    public bool HasWeather => !String.IsNullOrEmpty(WeatherOverview);
    public bool HasReservations => !String.IsNullOrEmpty(ReservationInfo);
    public bool HasRegulations => !String.IsNullOrEmpty(RegulationsOverview);
    public bool HasFees => (Fees is not null) && Fees.Count > 0;
    public bool HasOperatingHours => (OperatingHours is not null) && OperatingHours.Count > 0;
    public bool HasContacts => ((Contacts.PhoneNumbers is not null && Contacts.PhoneNumbers.Count > 0)) || ((Contacts.EmailAddresses is not null && Contacts.EmailAddresses.Count > 0));

    #endregion
}

public class Campsites
{
    public string TotalSites { get; set; }
    public string Group { get; set; }
    public string Horse { get; set; }
    public string TentOnly { get; set; }
    public string ElectricalHookup { get; set; }
    public string RvOnly { get; set; }
    public string WalkBoatTo { get; set; }
    public string Other { get; set; }
}
