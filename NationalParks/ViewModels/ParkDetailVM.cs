namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Park")]
public partial class ParkDetailVM : BaseVM
{
    [ObservableProperty]
    Park park;

    IMap map;

    public ParkDetailVM(IMap map)
    {
        this.map = map;
    }

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

    [RelayCommand]
    async Task GoToHours()
    {
        await Shell.Current.GoToAsync(nameof(ParkHoursPage), true, new Dictionary<string, object>
        {
            {"Park", Park }
        });
    }

    [RelayCommand]
    async Task GoToImages()
    {
        await Shell.Current.GoToAsync(nameof(ImagesPage), true, new Dictionary<string, object>
        {
            {"Park", Park }
        });
    }
}
