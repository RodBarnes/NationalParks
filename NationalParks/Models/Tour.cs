namespace NationalParks.Models;


public class Tour
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Park Park { get; set; }
    public List<string> Tags { get; set; }
    public List<Activity> Activities { get; set; }
    public List<Topic> Topics { get; set; }
    public string DurationMin { get; set; }
    public string DurationMax { get; set; }
    public string DurationUnit { get; set; }
    public List<Stop> Stops { get; set; }
    public List<Image> Images { get; set; }
    public ImageSource MainImage
    {
        get
        {
            if (Images.Count > 0)
                return ImageSource.FromUri(new Uri(Images.FirstOrDefault().Url));
            else
                return ImageSource.FromFile("no_image_green.png");
        }
    }
}
