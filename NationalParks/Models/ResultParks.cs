namespace NationalParks.Models;

public partial class ResultParks : Result
{
    public const string Term = "parks";
    public ICollection<Park> Data { get; set; }
}
