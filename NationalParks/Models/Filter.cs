namespace NationalParks.Models
{
    public class Filter
    {
        public List<Topic> Topics { get; set; } = new();
        public List<Activity> Activities { get; set; } = new();
        public List<State> States { get; set; } = new();
    }
}
