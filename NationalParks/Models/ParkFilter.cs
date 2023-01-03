namespace NationalParks.Models
{
    public class ParkFilter
    {
        public List<ParkTopic> Topics { get; set; } = new();
        public List<ParkActivity> Activities { get; set; } = new();
        public List<State> States { get; set; } = new();
        public List<Amenity> Amenities { get; set; } = new();
    }
}
