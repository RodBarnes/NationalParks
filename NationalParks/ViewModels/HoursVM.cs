namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Park), "Park")]
    public partial class HoursVM : BaseVM
    {
        [ObservableProperty]
        Park park;

        public ObservableCollection<Models.OperatingHours> Hours { get; } = new();

        public HoursVM()
        {
            Title = "Operating Hours";
        }

        public void PopulateData()
        {
            foreach (var hour in Park.OperatingHours)
            {
                Hours.Add(hour);
            }
        }
    }
}
