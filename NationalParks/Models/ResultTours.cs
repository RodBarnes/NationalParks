namespace NationalParks.Models;

public class ResultTours : Result
{
    public const string Term = "tours";
    public ICollection<Tour> Data { get; set; }
}
