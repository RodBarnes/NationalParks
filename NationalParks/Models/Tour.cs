namespace NationalParks.Models;


public class Tour
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public RelatedPark Park { get; set; }
    public List<string> Tags { get; set; }
    public List<Activity> Activities { get; set; }
    public List<Topic> Topics { get; set; }
    public string DurationMin { get; set; }
    public string DurationMax { get; set; }
    public string DurationUnit { get; set; }
    public List<Stop> Stops { get; set; }
    public List<Image> Images { get; set; }

    // Derived properties
    public ImageSource MainImage
    {
        get
        {
            ImageSource source = null;

            if (Images.Count > 0)
            {
                foreach (var image in Images)
                {
                    if (!String.IsNullOrEmpty(image.Url))
                    {
                        source = ImageSource.FromUri(new Uri(image.Url));
                    }
                }
            }

            source ??= ImageSource.FromFile("nps.png");

            return source;
        }
    }
    public bool HasTags => (Tags is not null) && Tags.Count > 0;
    public bool HasStops => (Stops is not null) && Stops.Count > 0;
    public bool HasTopics => (Topics is not null) && Topics.Count > 0;
    public bool HasActivities => (Activities is not null) && Activities.Count > 0;
}
