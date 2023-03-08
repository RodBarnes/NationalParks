namespace NationalParks.Models;

public class ResultTours : Result
{
    public const string Term = "tours";
    public List<Tour> Data { get; set; }
}
