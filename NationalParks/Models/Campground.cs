using System.Net.NetworkInformation;

namespace NationalParks.Models
{
    public class Campground
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string ParkCode { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AudioDescription { get; set; }
        public string IsPassportStampLocation { get; set; }
        public string PassportStampLocationDescription { get; set; }
        public List<object> PassportStampImages { get; set; }
        public string GeometryPoiId { get; set; }
        public string ReservationInfo { get; set; }
        public string ReservationUrl { get; set; }
        public string RegulationsUrl { get; set; }
        public string RegulationsOverview { get; set; }
        public CampgroundAmenities Amenities { get; set; }
        public Contacts Contacts { get; set; }
        public List<Fee> Fees { get; set; }
        public string DirectionsOverview { get; set; }
        public string DirectionsUrl { get; set; }
        public List<OperatingHours> OperatingHours { get; set; }
        public List<Address> Addresses { get; set; }
        public Address PhysicalAddress { get => Addresses.Where(a => a.Type == "Physical").FirstOrDefault(); }
        public List<Image> Images { get; set; }
        public ImageSource MainImage {
            get
            {
                if (Images.Count > 0)
                    return ImageSource.FromUri(new Uri(Images.FirstOrDefault().Url));
                else
                    return ImageSource.FromFile("no_image_green.png");
            }
        }
        public string WeatherOverview { get; set; }
        public string NumberOfSitesReservable { get; set; }
        public string NumberOfSitesFirstComeFirstServe { get; set; }
        public Campsite Campsites { get; set; }
        public CampgroundAccessibility Accessibility { get; set; }
        public List<Multimedia> Multimedia { get; set; }
        public string LastIndexDate { get; set; }

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
        public bool HasReservationUrl { get => !String.IsNullOrEmpty(ReservationUrl); }
        public bool HasRegulationsUrl { get => !String.IsNullOrEmpty(RegulationsUrl); }
        public bool HasDirections { get => !String.IsNullOrEmpty(DirectionsOverview) || (PhysicalAddress is not null); }
        public bool HasWeather { get => !String.IsNullOrEmpty(WeatherOverview); }
        public bool HasReservations { get => !String.IsNullOrEmpty(ReservationInfo); }
        public bool HasRegulations { get => !String.IsNullOrEmpty(RegulationsOverview); }
        public bool HasFees { get => (Fees is not null) && Fees.Count > 0; }
        public bool HasOperatingHours { get => (OperatingHours is not null) && OperatingHours.Count > 0; }
        public bool HasContacts { get => ((Contacts.PhoneNumbers is not null && Contacts.PhoneNumbers.Count > 0)) || ((Contacts.EmailAddresses is not null && Contacts.EmailAddresses.Count > 0)); }
    }
}
