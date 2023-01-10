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
