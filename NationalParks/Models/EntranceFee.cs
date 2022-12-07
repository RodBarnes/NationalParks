namespace NationalParks.Models
{
    public class EntranceFee
    {
        public string Cost { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }

        public double dCost
        {
            get
            {
                if (double.TryParse(Cost, out double d))
                {
                    return d;
                }
                else
                {
                    return -1;
                }
            }
        }
    }
}
