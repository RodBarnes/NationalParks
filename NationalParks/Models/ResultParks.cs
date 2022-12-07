namespace NationalParks.Models
{
    public class ResultParks
    {
        public string Total { get; set; }
        public string Limit { get; set; }
        public string Start { get; set; }
        public List<Park> Data { get; set; }
    }
}
