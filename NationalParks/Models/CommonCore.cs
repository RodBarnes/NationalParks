namespace NationalParks.Models;

public class CommonCore
{
    public string StateStandards { get; set; }
    public ICollection<string> MathStandards { get; set; }
    public string AdditionalStandards { get; set; }
    public ICollection<string> ElaStandards { get; set; }
}
