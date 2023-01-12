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
    async Task OpenMap(Dictionary<string, object> dict)
    {
        var latitude = (double)dict["Latitude"];
        var longitude = (double)dict["Longitude"];
        var name = (string)dict["Name"];

        if (latitude < 0)
        {
            await Shell.Current.DisplayAlert("No location", "Location coordinates are not provided.  Review the description for possible directions or related landmarks.", "OK");
            return;
        }

        try
        {
            await map.OpenAsync(latitude, longitude, new MapLaunchOptions
            {
                Name = name,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    static async Task GoToImages(Dictionary<string, object> dict)
    {
        var pageName = (string)dict["PageName"];
        var paramName = (string)dict["ParamName"];
        var obj = dict["Object"];

        await Shell.Current.GoToAsync(pageName, true, new Dictionary<string, object>
        {
            { paramName, obj }
        });
    }

    [RelayCommand]
    async Task GoToParkFromParkCode(Dictionary<string, object> dict)
    {
        var parkCode = (string)dict["ParkCode"];
        var paramName = (string)dict["ParamName"];

        Park park;

        ResultParks result = await DataService.GetParkForParkCodeAsync(parkCode);
        if (result.Data.Count == 1)
        {
            park = result.Data[0];
            await Shell.Current.GoToAsync(nameof(ParkDetailPage), true, new Dictionary<string, object>
                {
                    {paramName, park }
                });
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "Unable to get park!", "OK");
        }
    }
}
