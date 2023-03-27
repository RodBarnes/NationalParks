namespace NationalParks.Models;

internal class ResultNewsReleases : Result
{
    public const Terms Term = Terms.newsreleases;
    public List<NewsRelease> Data { get; set; }
}
