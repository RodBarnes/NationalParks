namespace NationalParks.Models;

public partial class ResultAudios : Result
{
    public const Terms Term = Terms.audio;
    public List<Multimedia> Data { get; set; }
}
