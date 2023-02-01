namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.NewsRelease), "Model")]
public partial class NewsReleaseDetailVM : DetailVM
{
    [ObservableProperty] NewsRelease newsRelease;

    public NewsReleaseDetailVM(IMap map) : base(map)
    {
        Title = "News Release";
    }
}
