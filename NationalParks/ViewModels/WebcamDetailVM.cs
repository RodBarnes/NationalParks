namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Webcam), "Model")]
public partial class WebcamDetailVM : DetailVM
{
    [ObservableProperty] Webcam webcam;
    [ObservableProperty] AvatarVM avatarVM;
    [ObservableProperty] RelatedParksVM relatedParksVM;

    public WebcamDetailVM(IMap map) : base(map)
    {
        Title = "Webcam";
    }

    [RelayCommand]
    public void PopulateData()
    {
        AvatarVM = new AvatarVM(Webcam.MainImage);

        RelatedParksVM = new RelatedParksVM("Related Parks", false, Webcam.RelatedParks);
    }
}
