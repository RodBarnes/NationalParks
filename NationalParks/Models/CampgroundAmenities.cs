namespace NationalParks.Models
{
    public class CampgroundAmenities
    {
        public string TrashRecyclingCollection { get; set; }
        public List<string> Toilets { get; set; }
        public string InternetConnectivity { get; set; }
        public List<string> Showers { get; set; }
        public string CellPhoneReception { get; set; }
        public string Laundry { get; set; }
        public string Amphitheater { get; set; }
        public string DumpStation { get; set; }
        public string CampStore { get; set; }
        public string StaffOrVolunteerHostOnsite { get; set; }
        public List<string> PotableWater { get; set; }
        public string IceAvailableForSale { get; set; }
        public string FirewoodForSale { get; set; }
        public string FoodStorageLockers { get; set; }

        public bool HasCellPhoneReception { get => !String.IsNullOrEmpty(CellPhoneReception); }
        public bool HasInternetConnectivity { get => !String.IsNullOrEmpty(InternetConnectivity); }

    }
}
