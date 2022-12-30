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
        public List<Activity> Activities { get; set; }
        public List<Topic> Topics { get; set; }
        public string States { get; set; }
        public Contacts Contacts { get; set; }
        public List<EntranceFee> EntranceFees { get; set; }
        public List<EntrancePass> EntrancePasses { get; set; }
        public List<Fee> Fees { get; set; }
        public string DirectionsInfo { get; set; }
        public string DirectionsUrl { get; set; }
        public List<OperatingHours> OperatingHours { get; set; }
        public List<Address> Addresses { get; set; }
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

        public bool HasTopics { get => !(Topics is null) && Topics.Count > 0; }
        public bool HasActivities { get => !(Activities is null) && Activities.Count > 0; }
        public bool HasDirections { get => !String.IsNullOrEmpty(DirectionsInfo); }
        public bool HasWeather { get => !String.IsNullOrEmpty(WeatherInfo); }
    }
}
