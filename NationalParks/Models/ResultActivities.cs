namespace NationalParks.Models;

public partial class ResultActivities : Result
{
    public const Terms Term = Terms.activities;
    public List<Activity> Data { get; set; }
}
