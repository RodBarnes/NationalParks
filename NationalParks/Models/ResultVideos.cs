namespace NationalParks.Models;

public partial class ResultVideos : Result
{
    public const string Term = "multimedia/videos";
    public ICollection<Multimedia> Data { get; set; }
}
