namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Park), "Park")]
    public partial class ParkHoursVM : BaseVM
    {
        // Query properties
        [ObservableProperty]
        Park park;

        public ObservableCollection<Models.OperatingHours> Hours { get; } = new();

        public ParkHoursVM()
        {
            Title = "Operating Hours";
        }

        public void PopulateData()
        {
            foreach (var hour in Park.OperatingHours)
                Hours.Add(hour);
        }
    }
}
