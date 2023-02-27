namespace NationalParks.Models;

public partial class ResultPeople : Result
{
    public const string Term = "people";
    public ICollection<Person> Data { get; set; }
}
