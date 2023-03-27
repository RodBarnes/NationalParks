namespace NationalParks.Models;

public partial class ResultVideos : Result
{
    public const Terms Term = Terms.videos;
    public List<Multimedia> Data { get; set; }
}
