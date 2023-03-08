namespace NationalParks.Models;

public class ResultPlaces : Result
{
    public const string Term = "places";
    public List<Place> Data { get; set; }
}
