namespace NationalParks.Models
{
    public class CombinedFee : Fee
    {
        public CombinedFee(string type, Fee fee) : base(fee)
        {
            Type = type;
        }

        public string Type { get; set; }
    }
}
