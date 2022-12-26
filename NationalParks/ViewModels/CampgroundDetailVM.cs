namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Models.Campground), "Campground")]
    public partial class CampgroundDetailVM : BaseVM
    {
        [ObservableProperty]
        Campground campground;

        IMap map;

        public CampgroundDetailVM(IMap map)
        {
            Title = "Campground";
            this.map = map;
        }
        [RelayCommand]
        async Task OpenMap()
        {
            try
            {
                await map.OpenAsync(Campground.DLatitude, Campground.DLongitude, new MapLaunchOptions
                {
                    Name = Campground.Name,
                    NavigationMode = NavigationMode.None
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");
                await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task GoToHours()
        {
            await Shell.Current.GoToAsync(nameof(CampgroundHoursPage), true, new Dictionary<string, object>
            {
                {"Campground", Campground }
            });
        }

        [RelayCommand]
        async Task GoToImages()
        {
            await Shell.Current.GoToAsync(nameof(CampgroundImageListPage), true, new Dictionary<string, object>
            {
                {"Campground", Campground }
            });
        }
    }
}
