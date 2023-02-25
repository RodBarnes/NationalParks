namespace NationalParks.Models;

public partial class ResultAudios : Result
{
    public const string Term = "multimedia/audio";
    public List<Multimedia> Data { get; set; }
}
