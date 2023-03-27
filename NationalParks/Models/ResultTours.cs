namespace NationalParks.Models;

public class ResultTours : Result
{
    public const Terms Term = Terms.tours;
    public List<Tour> Data { get; set; }
}
