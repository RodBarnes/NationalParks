namespace NationalParks.Models;

internal class ResultNewsReleases : Result
{
    public const string Term = "newsreleases";
    public ICollection<NewsRelease> Data { get; set; }
}
