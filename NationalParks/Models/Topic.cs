namespace NationalParks.Models;

public class Topic
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<RelatedPark> Parks { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
