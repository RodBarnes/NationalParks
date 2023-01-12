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
}
