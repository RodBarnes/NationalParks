namespace NationalParks.Models
{
    public class OperatingException
    {
        public Hours ExceptionHours { get; set; }
        public DateOnly StartDate { get; set; }
        public string Name { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
