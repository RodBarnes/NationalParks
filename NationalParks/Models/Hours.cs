namespace NationalParks.Models
{
    public class Hours
    {
        public string Wednesday { get; set; }
        public string Monday { get; set; }
        public string Thursday { get; set; }
        public string Sunday { get; set; }
        public string Tuesday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }

        public override string ToString()
        {
            return $"Mon:{Monday}\nTue:{Tuesday}\nWed:{Wednesday}\nThu:{Thursday}\nFri:{Friday}\nSat:{Saturday}\nSun:{Sunday}";
        }
    }
}
