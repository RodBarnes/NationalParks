namespace NationalParks.Models;

public partial class ResultTopics : Result
{
    public const Terms Term = Terms.topics;
    public List<Topic> Data { get; set; }
}
