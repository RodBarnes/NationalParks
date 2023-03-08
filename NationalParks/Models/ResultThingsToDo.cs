namespace NationalParks.Models;

public class ResultThingsToDo : Result
{
    public const string Term = "thingstodo";
    public List<ThingToDo> Data { get; set; }
}
