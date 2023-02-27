namespace NationalParks.Models;

public partial class ResultActivities : Result
{
    public const string Term = "activities";
    public ICollection<Activity> Data { get; set; }
}
