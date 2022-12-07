namespace NationalParks.Models
{
    public class ResultTopics
    {
        public string Total { get; set; }
        public string Limit { get; set; }
        public string Start { get; set; }
        public List<Topic> Data { get; set; }
    }
}
