namespace NationalParks.Models
{
    public class ResultEvents
    {
        public int Total { get; set; }
        public List<object> Errors { get; set; }
        public List<Event> Data { get; set; }
    }
}
