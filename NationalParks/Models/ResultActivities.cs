namespace NationalParks.Models
{
    public class ResultActivities
    {
        public string Total { get; set; }
        public string Limit { get; set; }
        public string Start { get; set; }
        public List<Activity> Data { get; set; }
    }
}
