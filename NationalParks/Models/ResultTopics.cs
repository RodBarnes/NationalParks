namespace NationalParks.Models;

public partial class ResultTopics : Result
{
    public const string Term = "topics";
    public List<Topic> Data { get; set; }
}
