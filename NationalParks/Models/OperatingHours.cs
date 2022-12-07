namespace NationalParks.Models
{
    public class OperatingHours
    {
        public List<ExceptionHours> ExceptionHours { get; set; }
        public string Description { get; set; }
        public StandardHours StandardHours { get; set; }
        public string Name { get; set; }
    }
}
