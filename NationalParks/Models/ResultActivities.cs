namespace NationalParks.Models
{
    public partial class ResultActivities : Result
    {
        public const string Term = "activities";
        public List<Activity> Data { get; set; }
    }
}
