namespace NationalParks.Models;

public class Park : BaseModel
{
    public string FullName { get; set; }
    public string ParkCode { get; set; }
    public string LatLong { get; set; }
    public ICollection<Alert> Alerts { get; set; }
    public ICollection<ParkingLot> ParkingLots { get; set; }
    public ICollection<Activity> Activities { get; set; }
    public ICollection<Topic> Topics { get; set; }
    public string States { get; set; }
    public Contacts Contacts { get; set; }
    public ICollection<Fee> EntranceFees { get; set; }
    public ICollection<Fee> EntrancePasses { get; set; }
    public ICollection<Fee> Fees { get; set; }
    public string DirectionsInfo { get; set; }
    public string DirectionsUrl { get; set; }
    public ICollection<OperatingHours> OperatingHours { get; set; }
    public ICollection<Address> Addresses { get; set; }
    public Address PhysicalAddress { get => Addresses.Where(a => a.Type == "Physical").FirstOrDefault(); }
    public string WeatherInfo { get; set; }
    public string Designation { get; set; }

    #region Derived Properties

    public new string Description { get => Designation; }
    public bool HasAlerts => (Alerts is not null) && Alerts.Count > 0;
    public bool HasTopics => (Topics is not null) && Topics.Count > 0;
    public bool HasActivities => (Activities is not null) && Activities.Count > 0;
    public bool HasFees => ((EntranceFees is not null) && EntranceFees.Count > 0) || ((EntrancePasses is not null) && EntrancePasses.Count > 0);
    public bool HasDirections => !String.IsNullOrEmpty(DirectionsInfo) || (PhysicalAddress is not null);
    public bool HasWeather => !String.IsNullOrEmpty(WeatherInfo);
    public bool HasOperatingHours => (OperatingHours is not null) && OperatingHours.Count > 0;
    public bool HasContacts => ((Contacts.PhoneNumbers is not null && Contacts.PhoneNumbers.Count > 0)) || ((Contacts.EmailAddresses is not null && Contacts.EmailAddresses.Count > 0));

    #endregion
}
