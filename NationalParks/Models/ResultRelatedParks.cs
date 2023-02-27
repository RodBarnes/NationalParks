namespace NationalParks.Models;

public partial class ResultRelatedParks : Result
{
    public const string Term = "topics/parks";
    public ICollection<RelatedPark> Data { get; set; }
}
