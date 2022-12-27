namespace NationalParks.Models
{
    public class ParkFilter
    {
        public List<Topic> Topics { get; set; } = new();
        public List<Activity> Activities { get; set; } = new();
        public List<State> States { get; set; } = new();
        public List<Amenity> Amenities { get; set; } = new();
    }
}
