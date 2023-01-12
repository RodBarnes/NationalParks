using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class ThingToDoDetailVM : BaseVM
{
    IMap map;

    [ObservableProperty] ThingToDo thingToDo;

    public ThingToDoDetailVM(IMap map)
    {
        Title = "Place";
        this.map = map;
    }

    [RelayCommand]
    async Task OpenMap()
    {
        if (ThingToDo.DLatitude < 0)
        {
            await Shell.Current.DisplayAlert("No location", "Location coordinates are not provided.  Review the description for possible directions or related landmarks.", "OK");
            return;
        }

        try
        {
            await map.OpenAsync(ThingToDo.DLatitude, ThingToDo.DLongitude, new MapLaunchOptions
            {
                Name = ThingToDo.Title,
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
        await Shell.Current.GoToAsync(nameof(PlaceImageListPage), true, new Dictionary<string, object>
        {
            {"ThingToDo", ThingToDo }
        });
    }

}
