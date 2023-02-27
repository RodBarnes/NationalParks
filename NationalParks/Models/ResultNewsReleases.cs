namespace NationalParks.Models;

internal class ResultNewsReleases : Result
{
    public const string Term = "newsreleases";
    public List<NewsRelease> Data { get; set; }
}
