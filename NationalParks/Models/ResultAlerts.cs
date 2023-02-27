namespace NationalParks.Models;

public class ResultAlerts : Result
{
    public const string Term = "alerts";
    public ICollection<Alert> Data { get; set; }
}
