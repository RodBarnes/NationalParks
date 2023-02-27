namespace NationalParks.Models;

public class ResultEvents : Result
{
    public const string Term = "events";
    public ICollection<Event> Data { get; set; }
}
