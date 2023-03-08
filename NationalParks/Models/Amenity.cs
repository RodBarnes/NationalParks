namespace NationalParks.Models;

public class Amenity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<string> Categories { get; set; }
}
