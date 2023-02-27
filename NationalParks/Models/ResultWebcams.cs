namespace NationalParks.Models;

public partial class ResultWebcams : Result
{
    public const string Term = "webcams";
    public ICollection<Webcam> Data { get; set; }
}
