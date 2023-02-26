namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Multimedia), "Model")]
public partial class MultimediaDetailVM : DetailVM
{
    [ObservableProperty] Multimedia multimedia;
    [ObservableProperty] RelatedParksVM relatedParks;
    [ObservableProperty] MultimediaVersionsVM multimediaVersions;
    [ObservableProperty] CollapsibleListVM tags;
    [ObservableProperty] CollapsibleTextVM transcript;

    public MultimediaDetailVM(IMap map) : base(map)
    {
        Title = "Multimedia";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Multimedia;

        Tags = new CollapsibleListVM("Tags", false, Multimedia.Tags.ToList<object>());
        MultimediaVersions = new MultimediaVersionsVM("Versions", false, Multimedia.Versions);
        RelatedParks = new RelatedParksVM("Related Parks", false, Multimedia.RelatedParks);
        Transcript = new CollapsibleTextVM("Transcript", false, Multimedia.Transcript);
    }
}
