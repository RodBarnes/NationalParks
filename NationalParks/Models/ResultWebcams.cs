namespace NationalParks.Models;

public partial class ResultWebcams : Result
{
    public const string Term = "webcams";
    public List<Webcam> Data { get; set; }
}
