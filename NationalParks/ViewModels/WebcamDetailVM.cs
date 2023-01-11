namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Models.Webcam), "Webcam")]
    public partial class WebcamDetailVM : BaseVM
    {
        IMap map;

        [ObservableProperty] Webcam webcam;

        public WebcamDetailVM(IMap map)
        {
            Title = "Webcam";
            this.map = map;
        }

        [RelayCommand]
        async Task OpenMap()
        {
            if (Webcam.Latitude < 0)
            {
                await Shell.Current.DisplayAlert("No location", "Location coordinates are not provided.  Review the description for possible directions or related landmarks.", "OK");
                return;
            }

            try
            {
                await map.OpenAsync((double)Webcam.Latitude, (double)Webcam.Longitude, new MapLaunchOptions
                {
                    Name = Webcam.Title,
                    NavigationMode = NavigationMode.None
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
            }
        }
    }
}
