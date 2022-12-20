namespace NationalParks.Models
{
    public class Park
    {
        public string Id { get; set; }
        public string URL { get; set; }
        public string FullName { get; set; }
        public string ParkCode { get; set; }
        public string Description { get; set; }
        private string Latitude { get; set; }
        private string Longitude { get; set; }
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
        public ImageSource MainImage { get => ImageSource.FromUri(new Uri(Images.FirstOrDefault().Url)); }
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

    }
}
