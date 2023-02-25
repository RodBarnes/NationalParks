namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Multimedia), "Model")]
public partial class MultimediaDetailVM : DetailVM
{
    [ObservableProperty] Multimedia multimedia;

    public MultimediaDetailVM(IMap map) : base(map)
    {
        Title = "Multimedia";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Multimedia;
    }
}
