namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Place), "Place")]
public partial class PlaceDetailVM : BaseVM
{
    IMap map;

    [ObservableProperty]
    Place place;

    public PlaceDetailVM(IMap map)
    {
        Title = "Place";
        this.map = map;

    }
}
