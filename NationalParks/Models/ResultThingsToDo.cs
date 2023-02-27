namespace NationalParks.Models;

public class ResultThingsToDo : Result
{
    public const string Term = "thingstodo";
    public ICollection<ThingToDo> Data { get; set; }
}
