namespace NationalParks.Models
{
    public class Park
    {
        public string Id { get; set; }
        public string URL { get; set; }
        public string FullName { get; set; }
        public string ParkCode { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LatLong { get; set; }
        public List<Alert> Alerts { get; set; } = new();
        public List<Activity> Activities { get; set; } = new();
        public List<Topic> Topics { get; set; } = new();
        public string States { get; set; }
        public Contacts Contacts { get; set; }
        public List<Fee> EntranceFees { get; set; }
        public List<Fee> EntrancePasses { get; set; }
        public List<Fee> Fees { get; set; }
        public string DirectionsInfo { get; set; }
        public string DirectionsUrl { get; set; }
        public List<OperatingHours> OperatingHours { get; set; }
        public List<Address> Addresses { get; set; }
        public Address PhysicalAddress { get => Addresses.Where(a => a.Type == "Physical").FirstOrDefault(); }
        public ImageSource MainImage
        {
            get
            {
                if (Images.Count > 0)
                    return ImageSource.FromUri(new Uri(Images.FirstOrDefault().Url));
                else
                    return ImageSource.FromFile("no_image_green.png");
            }
        }
        public List<Image> Images { get; set; }
        public string WeatherInfo { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }

        // Derived properties
        public double DLatitude
        {
            get
            {
                if (double.TryParse(Latitude, out double d))
                {
                    return d;
                }
                else
                {
                    return -1;
                }
            }
        }
        public double DLongitude
        {
            get
            {
                if (double.TryParse(Longitude, out double d))
                {
                    return d;
                }
                else
                {
                    return -1;
                }
            }
        }
        public bool HasParkUrl { get => !String.IsNullOrEmpty(URL); }
        public bool HasAlerts { get => (Alerts is not null) && Alerts.Count > 0; }
        public bool HasTopics { get => (Topics is not null) && Topics.Count > 0; }
        public bool HasActivities { get => (Activities is not null) && Activities.Count > 0; }
        public bool HasEntranceFees { get => (EntranceFees is not null) && EntranceFees.Count > 0; }
        public bool HasDirections { get => !String.IsNullOrEmpty(DirectionsInfo) || (PhysicalAddress is not null); }
        public bool HasWeather { get => !String.IsNullOrEmpty(WeatherInfo); }
        public bool HasOperatingHours { get => (OperatingHours is not null) && OperatingHours.Count > 0; }
        public bool HasContacts { get => ((Contacts.PhoneNumbers is not null && Contacts.PhoneNumbers.Count > 0)) || ((Contacts.EmailAddresses is not null && Contacts.EmailAddresses.Count > 0)); }
    }
}
