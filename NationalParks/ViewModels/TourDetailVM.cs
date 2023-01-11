using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Tour), "Tour")]
public partial class TourDetailVM : BaseVM
{
    DataService dataService;
    IMap map;

    [ObservableProperty] Tour tour;

    [ObservableProperty] CollapsibleViewVM tagsVM;

    [ObservableProperty] CollapsibleViewVM stopsVM;

    [ObservableProperty] CollapsibleViewVM topicsVM;

    [ObservableProperty] CollapsibleViewVM activitiesVM;

    public TourDetailVM(DataService dataService, IMap map)
    {
        Title = "Tour";
        this.map = map;
        this.dataService = dataService;

        TopicsVM = new CollapsibleViewVM("Topics", false);
        ActivitiesVM = new CollapsibleViewVM("Activities", false);
        TagsVM = new CollapsibleViewVM("Tags", false);
        StopsVM = new CollapsibleViewVM("Stops", false);
    }

    [RelayCommand]
    async Task OpenMap()
    {
        try
        {
            await map.OpenAsync(Tour.DLatitude, Tour.DLongitude, new MapLaunchOptions
            {
                Name = Tour.Title,
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
        await Shell.Current.GoToAsync(nameof(TourImageListPage), true, new Dictionary<string, object>
        {
            {"Tour", Tour }
        });
    }

    [RelayCommand]
    async Task GoToParkFromRelatedParkAsync(RelatedPark relPark)
    {
        if (relPark == null)
            return;

        Park park;

        ResultParks result = await DataService.GetParkForParkCodeAsync(relPark.ParkCode);
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
