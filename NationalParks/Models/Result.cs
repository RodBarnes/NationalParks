namespace NationalParks.Models;

public partial class Result
{
    public int Total { get; set; }
    public int Limit { get; set; }
    public int Start { get; set; }
}

public enum Terms
{
    activities,
    alerts,
    articles,
    audio,
    campgrounds,
    events,
    newsreleases,
    parkinglots,
    parks,
    people,
    places,
    thingstodo,
    topics,
    tours,
    videos,
    webcams
}