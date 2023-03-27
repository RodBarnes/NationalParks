namespace NationalParks.Models;

public partial class ResultPeople : Result
{
    public const Terms Term = Terms.people;
    public List<Person> Data { get; set; }
}
