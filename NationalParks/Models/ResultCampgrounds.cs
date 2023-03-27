namespace NationalParks.Models;

public partial class ResultCampgrounds : Result
{
    public const Terms Term = Terms.campgrounds;
    public List<Campground> Data { get; set; }
}
