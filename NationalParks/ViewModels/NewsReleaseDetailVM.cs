namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.NewsRelease), "Model")]
public partial class NewsReleaseDetailVM : DetailVM
{
    [ObservableProperty] NewsRelease newsRelease;
    [ObservableProperty] AvatarVM avatarVM;
    [ObservableProperty] RelatedParksVM relatedParksVM;
    [ObservableProperty] CollapsibleListVM organizationsVM;

    public NewsReleaseDetailVM(IMap map) : base(map)
    {
        Title = "News Release";
    }

    [RelayCommand]
    public void PopulateData()
    {
        AvatarVM = new AvatarVM(NewsRelease.MainImage);

        OrganizationsVM = new CollapsibleListVM("Related Organizations", false, NewsRelease.RelatedOrganizations.ToList<object>());

        RelatedParksVM = new RelatedParksVM("Related Parks", false, NewsRelease.RelatedParks);
    }
}
