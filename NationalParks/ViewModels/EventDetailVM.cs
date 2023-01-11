using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Event), "Event")]
public partial class EventDetailVM : BaseVM
{
    DataService dataService;
    IMap map;

    [ObservableProperty] Event npsEvent;

    public EventDetailVM(DataService dataService, IMap map)
    {
        Title = "Events";
        this.dataService = dataService;
        this.map = map;
    }

    [RelayCommand]
    async Task OpenMap()
    {
        try
        {
            await map.OpenAsync(NpsEvent.DLatitude, NpsEvent.DLongitude, new MapLaunchOptions
            {
                Name = NpsEvent.Title,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task GoToImages()
    {
        await Shell.Current.GoToAsync(nameof(ParkImageListPage), true, new Dictionary<string, object>
        {
            {"Event", NpsEvent }
        });
    }

    [RelayCommand]
    async Task GoToParkFromParkCodeAsync()
    {
        Park park;

        ResultParks result = await dataService.GetParkForParkCodeAsync(npsEvent.SiteCode);
        if (result.Data.Count == 1)
        {
            park = result.Data[0];
            await Shell.Current.GoToAsync(nameof(ParkDetailPage), true, new Dictionary<string, object>
                {
                    {"Park", park }
                });
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "Unable to get park!", "OK");
        }
    }
}
