namespace NationalParks.Models
{
    public class OperatingException
    {
        public Hours ExceptionHours { get; set; }
        public DateTime StartDate { get; set; }
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
    }
}
