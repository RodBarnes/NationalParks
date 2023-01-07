namespace NationalParks.Models
{
    public class Park
    {
        public string Id { get; set; }
        public string Url { get; set; }
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
        public List<Image> Images { get; set; }
        public string WeatherInfo { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }

        // Derived properties
        public ImageSource MainImage
        {
            get
            {
                ImageSource source = null;

                if (Images.Count > 0)
                {
                    foreach (var image in Images)
                    {
                        if (!String.IsNullOrEmpty(image.Url))
                        {
                            source = ImageSource.FromUri(new Uri(image.Url));
                        }
                    }
                }

                source ??= ImageSource.FromFile("nps.png");

                return source;
            }
        }
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
        public bool HasUrl => !String.IsNullOrEmpty(Url);
        public bool HasAlerts => (Alerts is not null) && Alerts.Count > 0;
        public bool HasTopics => (Topics is not null) && Topics.Count > 0;
        public bool HasActivities => (Activities is not null) && Activities.Count > 0;
        public bool HasFees => ((EntranceFees is not null) && EntranceFees.Count > 0) || ((EntrancePasses is not null) && EntrancePasses.Count > 0);
        public bool HasDirections => !String.IsNullOrEmpty(DirectionsInfo) || (PhysicalAddress is not null);
        public bool HasWeather => !String.IsNullOrEmpty(WeatherInfo);
        public bool HasOperatingHours => (OperatingHours is not null) && OperatingHours.Count > 0;
        public bool HasContacts => ((Contacts.PhoneNumbers is not null && Contacts.PhoneNumbers.Count > 0)) || ((Contacts.EmailAddresses is not null && Contacts.EmailAddresses.Count > 0));
    }
}
