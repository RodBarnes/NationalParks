namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Tour), "Tour")]
public partial class TourDetailVM : BaseVM
{
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

    public TourDetailVM()
    {
        Title = "Tour";

        TopicsVM = new CollapsibleViewVM("Topics", false);
        ActivitiesVM = new CollapsibleViewVM("Activities", false);
        TagsVM = new CollapsibleViewVM("Tags", false);
        StopsVM = new CollapsibleViewVM("Stops", false);
    }

    //[RelayCommand]
    //async Task OpenMap()
    //{
    //    try
    //    {
    //        await map.OpenAsync(Tour.DLatitude, Tour.DLongitude, new MapLaunchOptions
    //        {
    //            Name = Tour.Name,
    //            NavigationMode = NavigationMode.None
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine($"Unable to launch maps: {ex.Message}");
    //        await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
    //    }
    //}

    [RelayCommand]
    async Task GoToImages()
    {
        await Shell.Current.GoToAsync(nameof(ParkImageListPage), true, new Dictionary<string, object>
        {
            {"Tour", Tour }
        });
    }
}
