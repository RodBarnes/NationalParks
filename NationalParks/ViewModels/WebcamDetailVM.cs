namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Models.Webcam), "Webcam")]
    public partial class WebcamDetailVM : BaseVM
    {
        [ObservableProperty]
        Webcam webcam;

        public WebcamDetailVM()
        {
            Title = "Webcam";
        }
    }
}
