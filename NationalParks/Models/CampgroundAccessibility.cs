namespace NationalParks.Models
{
    public class CampgroundAccessibility
    {
        public string WheelchairAccess { get; set; }
        public string InternetInfo { get; set; }
        public string CellPhoneInfo { get; set; }
        public string FireStovePolicy { get; set;}
        public string RvAllowed { get; set;}
        public string RvInfo { get; set;}
        public string RvMaxLength { get; set;}
        public string AdditionalInfo { get; set;}
        public string TrailerMaxLength { get; set;}
        public string AdaInfo { get; set;}
        public string TrailerAllowed { get; set;}
        public List<string> AccessRoads { get; set;}
        public List<string> Classifications { get; set;}
    }
}
