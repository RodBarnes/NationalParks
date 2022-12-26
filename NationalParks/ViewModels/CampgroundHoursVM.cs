namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Campground), "Campground")]
    public partial class CampgroundHoursVM : BaseVM
    {
        // Query properties
        [ObservableProperty]
        Campground campground;

        public ObservableCollection<Models.OperatingHours> Hours { get; } = new();

        public CampgroundHoursVM()
        {
            Title = "Hours";
        }


        public void PopulateData()
        {
            foreach (var hour in Campground.OperatingHours)
                Hours.Add(hour);
        }
    }
}
