namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Park")]
public partial class ParkVM : BaseVM
{
    public ObservableCollection<Models.Image> Images { get; set; }

    [ObservableProperty]
    Park park;

    IMap map;

    public ParkVM(IMap map)
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
        await Shell.Current.GoToAsync(nameof(HoursPage), true, new Dictionary<string, object>
        {
            {"Hours", park.OperatingHours }
        });
    }

    [RelayCommand]
    async Task GoToImages()
    {
        await Shell.Current.GoToAsync(nameof(ImagesPage), true, new Dictionary<string, object>
        {
            {"Images", park.Images }
        });
    }
}
