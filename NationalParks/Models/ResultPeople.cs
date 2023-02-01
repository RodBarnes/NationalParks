namespace NationalParks.Models;

public partial class ResultPeople : Result
{
    public const string Term = "people";
    public List<Person> Data { get; set; }
}
