using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class DetailVM : BaseVM
{
    readonly IMap map;

    public DetailVM(IMap map)
    {
        this.map = map;
    }

    [RelayCommand]
    async Task OpenMap(BaseModel model)
    {
        if (model.DLatitude < 0)
        {
            await Shell.Current.DisplayAlert("No location", "Location coordinates are not provided.  Review the description for possible directions or related landmarks.", "OK");
            return;
        }

        try
        {
            await map.OpenAsync(model.DLatitude, model.DLongitude, new MapLaunchOptions
            {
                Name = model.Title,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    static async Task GoToImages(List<Models.Image> images)
    {
        await Shell.Current.GoToAsync(nameof(ImageListPage), true, new Dictionary<string, object>
        {
            { "Images", images }
        });
    }

    [RelayCommand]
    async Task GoToParkFromParkCode(string parkCode)
    {
        Park park;

        ResultParks result = await DataService.GetParkForParkCodeAsync(parkCode);
        if (result.Data.Count > 0)
        {
            park = result.Data[0];
            await Shell.Current.GoToAsync(nameof(ParkDetailPage), true, new Dictionary<string, object>
                {
                    {"Model", park }
                });
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "Unable to get park!", "OK");
        }
    }
}
