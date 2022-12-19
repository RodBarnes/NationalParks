namespace NationalParks.Models
{
    public class ExceptionHours
    {
        public DateOnly StartDate { get; set; }
        public string Name { get; set; }
        public DateOnly EndDate { get; set; }
        public string Combined
        {
            get
            {
                return $"{Name}: {StartDate}-{EndDate}";
            }
        }
    }
}
