namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Park")]
public partial class DetailsVM : BaseVM
{
    IMap map;
    public DetailsVM(IMap map)
    {
        this.map = map;
    }

    [ObservableProperty]
    Park park;

    [RelayCommand]
    async Task OpenMap()
    {
        try
        {
            await map.OpenAsync(Park.DLatitude, Park.DLongitude, new MapLaunchOptions
            {
                Name = Park.Name,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to launch maps: {ex.Message}");
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }
}
