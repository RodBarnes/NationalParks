namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Webcam), "Model")]
public partial class WebcamDetailVM : DetailVM
{
    [ObservableProperty] Webcam webcam;
    [ObservableProperty] RelatedParksVM relatedParksVM;

    public WebcamDetailVM(IMap map) : base(map)
    {
        Title = "Webcam";
        RelatedParksVM = new RelatedParksVM("Related Parks", false);
    }
    [RelayCommand]
    public void PopulateData()
    {
        RelatedParksVM.HasContent = Webcam.HasRelatedParks;
        RelatedParksVM.Items = Webcam.RelatedParks;
    }
}
