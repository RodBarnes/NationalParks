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

    [RelayCommand]
    async Task OpenMap()
    {
        try
        {
            await map.OpenAsync(Place.DLatitude, Place.DLongitude, new MapLaunchOptions
            {
                Name =  Place.Title,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to launch maps: {ex.Message}");
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task GoToImages()
    {
        await Shell.Current.GoToAsync(nameof(ParkImageListPage), true, new Dictionary<string, object>
        {
            {"Place", Place }
        });
    }

}
