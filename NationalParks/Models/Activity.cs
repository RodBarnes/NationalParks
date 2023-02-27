namespace NationalParks.Models;

public class Activity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<RelatedPark> Parks { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
