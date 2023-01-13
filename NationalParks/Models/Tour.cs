namespace NationalParks.Models;


public class Tour : BaseModel
{
    public string Description { get; set; }
    public RelatedPark Park { get; set; }
    public List<string> Tags { get; set; }
    public List<Activity> Activities { get; set; }
    public List<Topic> Topics { get; set; }
    public int DurationMin { get; set; }
    public int DurationMax { get; set; }
    public string DurationUnit { get; set; }
    public List<Stop> Stops { get; set; }

    // Derived properties
    public string Duration => $"{DurationMin}-{DurationMax}{DurationUnit}";
    public bool HasTags => (Tags is not null) && Tags.Count > 0;
    public bool HasStops => (Stops is not null) && Stops.Count > 0;
    public bool HasTopics => (Topics is not null) && Topics.Count > 0;
    public bool HasActivities => (Activities is not null) && Activities.Count > 0;
}
