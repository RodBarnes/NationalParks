namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Multimedia), "Model")]
public partial class MultimediaDetailVM : DetailVM
{
    [ObservableProperty] Multimedia multimedia;
    [ObservableProperty] RelatedParksVM relatedParksVM;
    [ObservableProperty] MultimediaVersionsVM multimediaVersionsVM;
    [ObservableProperty] CollapsibleListVM tagsVM;
    [ObservableProperty] CollapsibleTextVM transcript;

    public MultimediaDetailVM(IMap map) : base(map)
    {
        Title = "Multimedia";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Multimedia;

        TagsVM = new CollapsibleListVM("Tags", false, Multimedia.Tags.ToList<object>());
        MultimediaVersionsVM = new MultimediaVersionsVM("Versions", false, Multimedia.Versions);
        RelatedParksVM = new RelatedParksVM("Related Parks", false, Multimedia.RelatedParks);
        Transcript = new CollapsibleTextVM("Transcript", false, Multimedia.Transcript);
    }
}
