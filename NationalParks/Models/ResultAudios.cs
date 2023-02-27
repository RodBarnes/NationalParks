namespace NationalParks.Models;

public partial class ResultAudios : Result
{
    public const string Term = "multimedia/audio";
    public ICollection<Multimedia> Data { get; set; }
}
