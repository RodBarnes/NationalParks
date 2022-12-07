namespace NationalParks.Models
{
    public class Result
    {
        public string Total { get; set; }
        public string Limit { get; set; }
        public string Start { get; set; }
        public List<Park> Data { get; set; }
    }
}
