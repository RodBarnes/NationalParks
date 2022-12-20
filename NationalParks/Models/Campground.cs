namespace NationalParks.Models
{
    public class Campground
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string ParkCode { get; set; }
        private string Latitude { get; set; }
        private string Longitude { get; set; }
        public string AudioDescription { get; set; }
        public string IsPassportStampLocation { get; set; }
        public string PassportStampLocationDescription { get; set; }
        public List<object> PassportStampImages { get; set; }
        public string GeometryPoiId { get; set; }
        public string ReservationInfo { get; set; }
        public string ReservationUrl { get; set; }
        public string RegulationsUrl { get; set; }
        public string RegulationsOverview { get; set; }
        public List<CampgroundAmenity> Amentities { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<string> Fees { get; set; }
        public string DirectionsOverview { get; set; }
        public string DirectionsUrl { get; set; }
        public List<OperatingHours> OperatingHours { get; set; }
        public List<Address> Addresses { get; set; }
        public List<Image> Images { get; set; }
        public string WeatherOverview { get; set; }
        public string NumberOfSitesReservable { get; set; }
        public string NumberOfSitesFirstComeFirstServe { get; set; }
        public List<Campsite> Campsites { get; set; }
        public List<Accessibility> Accessibility { get; set; }
        public List<object> Multimedia { get; set; }
        public string LastIndexDate { get; set; }

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
