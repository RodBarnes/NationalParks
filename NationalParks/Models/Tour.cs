namespace NationalParks.Models;


public class Tour
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ParkFull Park { get; set; }
    public object[] Tags { get; set; }
    public Activity[] Activities { get; set; }
    public Topic[] Topics { get; set; }
    public string DurationMin { get; set; }
    public string DurationMax { get; set; }
    public string DurationUnit { get; set; }
    public Stop[] Stops { get; set; }
    public Image[] Images { get; set; }
}
