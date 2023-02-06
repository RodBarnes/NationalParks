namespace NationalParks.Models;

public class Stop
{
    public string Significance { get; set; }
    public string AssetId { get; set; }
    public string AssetName { get; set; }
    public string AssetType { get; set; }
    public string Id { get; set; }
    public string Ordinal { get; set; }
    public string DirectionsToNextStop { get; set; }
    public string AudioTranscript { get; set; }
    public string AudioFileUrl { get; set; }

    public override string ToString()
    {
        return AssetName;
    }
}
