namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Event), "Event")]
public partial class EventDetailVM : BaseVM
{
    IMap map;

    [ObservableProperty] Event npsEvent;

    public EventDetailVM(IMap map)
    {
        Title = "Events";
        this.map = map;
    }

    [RelayCommand]
    async Task OpenMap()
    {
        if (NpsEvent.DLatitude < 0)
        {
            await Shell.Current.DisplayAlert("No location", "Location coordinates are not provided.  Review the description for possible directions or related landmarks.", "OK");
            return;
        }

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
}
