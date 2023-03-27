using NationalParks.Services;
using System.Reflection;

namespace NationalParks.ViewModels;

public partial class DetailVM : BaseVM
{
    readonly IMap map;
    [ObservableProperty] BaseModel model;

    public DetailVM(IMap map)
    {
        this.map = map;
    }

    [RelayCommand]
    async Task OpenMap(BaseModel model)
    {
        if (model.DLatitude < 0)
        {
            await Shell.Current.DisplayAlert("No location", $"{model.Title} does not provide any location coordinates.  Review the description for possible directions or related landmarks.", "OK");
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
            await Utility.HandleException(ex, new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
        }
    }

    [RelayCommand]
    static async Task GoToImages(List<Models.Image> images)
    {
        try
        {
            if (images is null || images.Count == 0)
            {
                await Shell.Current.DisplayAlert("No images", "No images were found.", "OK");
                return;
            }

            await Shell.Current.GoToAsync(nameof(ImageListPage), true, new Dictionary<string, object>
            {
                { "Images", images }
            });
        }
        catch (Exception ex)
        {
            await Utility.HandleException(ex, new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
        }
    }

    [RelayCommand]
    async Task GoToParkFromParkCode(string parkCode)
    {
        Park park;

        try
        {
            ResultParks result = await DataService.GetItemsForParkCodeAsync<ResultParks>(ResultParks.Term, parkCode);
            if (result.Data.Count > 0)
            {
                park = result.Data.First();
                park.FillMainImage();
                await Shell.Current.GoToAsync(nameof(ParkDetailPage), true, new Dictionary<string, object>
                {
                    {"Model", park }
                });
            }
            else
            {
                await Shell.Current.DisplayAlert("Error!", $"Unable to get park for {parkCode}!", "OK");
            }
        }
        catch (Exception ex)
        {
            await Utility.HandleException(ex, new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
        }
    }


    protected async Task<string> GetNameFromParkCode(string parkCode)
    {
        string name = "";

        try
        {
            ResultParks result = await DataService.GetItemsForParkCodeAsync<ResultParks>(ResultParks.Term, parkCode);
            if (result.Data.Count > 0)
            {
                var park = result.Data.First();
                name = park.Name;
            }
        }
        catch (Exception ex)
        {
            await Utility.HandleException(ex, new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
        }

        return name;
    }
}
