namespace NationalParks.Models;

public partial class ResultCampgrounds : Result
{
    public const string Term = "campgrounds";
    public ICollection<Campground> Data { get; set; }
}
