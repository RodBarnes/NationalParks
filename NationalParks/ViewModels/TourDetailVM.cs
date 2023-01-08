namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Tour), "Tour")]
public partial class TourDetailVM : BaseVM
{
    IMap map;

    [ObservableProperty]
    Tour tour;

    [ObservableProperty]
    public CollapsibleViewVM tagsVM;

    [ObservableProperty]
    public CollapsibleViewVM stopsVM;

    [ObservableProperty]
    public CollapsibleViewVM topicsVM;

    [ObservableProperty]
    public CollapsibleViewVM activitiesVM;

    public TourDetailVM(IMap map)
    {
        Title = "Tour";
        this.map = map;

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
        await Shell.Current.GoToAsync(nameof(ParkImageListPage), true, new Dictionary<string, object>
        {
            {"Tour", Tour }
        });
    }
}
