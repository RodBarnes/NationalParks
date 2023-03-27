namespace NationalParks.Models;

public class ResultEvents : Result
{
    public const Terms Term = Terms.events;
    public List<Event> Data { get; set; }
}
