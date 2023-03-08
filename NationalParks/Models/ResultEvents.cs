namespace NationalParks.Models;

public class ResultEvents : Result
{
    public const string Term = "events";
    public List<Event> Data { get; set; }
}
