namespace NationalParks.Models;

public partial class ResultWebcams : Result
{
    public const Terms Term = Terms.webcams;
    public List<Webcam> Data { get; set; }
}
