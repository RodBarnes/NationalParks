namespace NationalParks.Models;

public class ResultAlerts : Result
{
    public const Terms Term = Terms.alerts;
    public List<Alert> Data { get; set; }
}
