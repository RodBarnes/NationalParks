namespace NationalParks.Models;

public class ParkAlert
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string ParkCode { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string LastIndexedDate { get; set; }
}
