using NationalParks.Services;

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
            await Shell.Current.DisplayAlert("Error!", $"Unable to open Maps: {ex.Message}.", "OK");
        }
    }

    [RelayCommand]
    static async Task GoToImages(List<Models.Image> images)
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

    [RelayCommand]
    async Task GoToParkFromParkCode(string parkCode)
    {
        Park park;

        ResultParks result = await DataService.GetParkForParkCodeAsync(parkCode);
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
}
