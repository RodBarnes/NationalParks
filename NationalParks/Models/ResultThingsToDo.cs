namespace NationalParks.Models;

public class ResultThingsToDo : Result
{
    public const Terms Term = Terms.thingstodo;
    public List<ThingToDo> Data { get; set; }
}
