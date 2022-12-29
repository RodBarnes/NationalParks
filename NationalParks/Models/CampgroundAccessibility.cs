namespace NationalParks.Models
{
    public class CampgroundAccessibility
    {
        public string AdaInfo { get; set; }
        public string WheelchairAccess { get; set; }
        public string InternetInfo { get; set; }
        public string CellPhoneInfo { get; set; }
        public string FireStovePolicy { get; set;}
        public string RvAllowed { get; set;}
        public string RvInfo { get; set;}
        public string RvMaxLength { get; set;}
        public string TrailerAllowed { get; set; }
        public string TrailerMaxLength { get; set;}
        public string AdditionalInfo { get; set; }
        public List<string> AccessRoads { get; set;}
        public List<string> Classifications { get; set;}

        public bool HasCellPhoneInfo { get => !String.IsNullOrEmpty(CellPhoneInfo); }
        public bool HasInternetInfo { get => !String.IsNullOrEmpty(InternetInfo); }
        public bool HasTrailerMaxLength
        {
            get
            {
                if (String.IsNullOrEmpty(TrailerMaxLength))
                    return false;

                if (!int.TryParse(TrailerMaxLength, out int ln))
                    return false;

                if (ln <= 0)
                    return false;

                return true;
            }
        }

        public bool HasRvMaxLength
        {
            get
            {
                if (String.IsNullOrEmpty(RvMaxLength))
                    return false;

                if (!int.TryParse(RvMaxLength, out int ln))
                    return false;

                if (ln <= 0)
                    return false;

                return true;
            }
        }
    }
}
