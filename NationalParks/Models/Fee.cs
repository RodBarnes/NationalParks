namespace NationalParks.Models;

public class Fee
{
    public Fee() { }

    public Fee(Fee fee)
    {
        Cost = fee.Cost;
        Description = fee.Description;
        Title = fee.Title;
    }

    public string Cost { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
}
