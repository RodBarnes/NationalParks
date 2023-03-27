namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Webcam), "Model")]
public partial class WebcamDetailVM : DetailVM
{
    [ObservableProperty] Webcam webcam;
    [ObservableProperty] RelatedParksVM relatedParks;

    public WebcamDetailVM(IMap map) : base(map)
    {
        Title = "Webcam";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Webcam;

        RelatedParks = new RelatedParksVM("Related Parks", false, Webcam.RelatedParks);
    }

    [RelayCommand]
    public async Task GoToWebcam()
    {
        await Launcher.OpenAsync(Webcam.Url);
    }
}
