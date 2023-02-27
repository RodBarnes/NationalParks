namespace NationalParks.Models;

public class OperatingHours
{
    public ICollection<OperatingException> Exceptions { get; set; }
    public string Description { get; set; }
    public Hours StandardHours { get; set; }
    public string Name { get; set; }
}
