namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.NewsRelease), "Model")]
public partial class NewsReleaseDetailVM : DetailVM
{
    [ObservableProperty] NewsRelease newsRelease;
    [ObservableProperty] AvatarVM avatarVM;

    public NewsReleaseDetailVM(IMap map) : base(map)
    {
        Title = "News Release";
    }

    [RelayCommand]
    public void PopulateData()
    {
        AvatarVM = new AvatarVM(NewsRelease.MainImage);
    }
}
