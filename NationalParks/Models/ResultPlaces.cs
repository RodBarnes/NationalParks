namespace NationalParks.Models;

public class ResultPlaces : Result
{
    public const Terms Term = Terms.places;
    public List<Place> Data { get; set; }
}
