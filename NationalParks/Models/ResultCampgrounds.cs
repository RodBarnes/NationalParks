namespace NationalParks.Models
{
    public partial class ResultCampgrounds : Result
    {
        public const string Term = "campgrounds";
        public List<Campground> Data { get; set; }
    }
}
