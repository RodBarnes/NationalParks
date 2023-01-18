namespace NationalParks.Models
{
    public class ResultAlerts : Result
    {
        public const string Term = "alerts";
        public List<Alert> Data { get; set; }
    }
}
