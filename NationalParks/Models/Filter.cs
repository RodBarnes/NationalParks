namespace NationalParks.Models
{
    public class Filter
    {
        public List<Topic> Topics { get; set; }
        public List<Activity> Activities { get; set; }
        public List<State> States { get; set; }
        public List<Amenity> Amenities { get; set; }
    }
}
