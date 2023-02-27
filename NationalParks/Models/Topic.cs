namespace NationalParks.Models;

public class Topic
{
    public string Id { get; set; }
    public string Name { get; set; }
    //public ICollection<RelatedPark> RelatedParks { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
