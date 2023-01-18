namespace NationalParks.Models
{
    public partial class ResultParks : Result
    {
        public const string Term = "parks";
        public List<Park> Data { get; set; }
    }
}
