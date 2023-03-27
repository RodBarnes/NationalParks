namespace NationalParks.Models;

public partial class ResultParks : Result
{
    public const Terms Term = Terms.parks;
    public List<Park> Data { get; set; }
}
