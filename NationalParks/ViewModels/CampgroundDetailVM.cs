namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Models.Campground), "Campground")]
    public partial class CampgroundDetailVM : BaseVM
    {
        [ObservableProperty]
        Campground campground;

        public CampgroundDetailVM()
        {
            Title = "Campground";
        }
    }
}
