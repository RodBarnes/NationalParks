namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Models.Webcam), "Webcam")]
    public partial class WebcamDetailVM : BaseVM
    {
        [ObservableProperty]
        Webcam webcam;

        IMap map;

        public WebcamDetailVM(IMap map)
        {
            Title = "Webcam";
            this.map = map;
        }

        [RelayCommand]
        async Task OpenMap()
        {
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
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");
                await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
            }
        }
    }
}
