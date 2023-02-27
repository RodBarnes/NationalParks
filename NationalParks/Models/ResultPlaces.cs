namespace NationalParks.Models;

public class ResultPlaces : Result
{
    public const string Term = "places";
    public ICollection<Place> Data { get; set; }
}
