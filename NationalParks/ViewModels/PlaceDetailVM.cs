using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Place), "Place")]
public partial class PlaceDetailVM : BaseVM
{
    IMap map;
    DataService dataService;

    [ObservableProperty]
    Place place;

    [ObservableProperty]
    public CollapsibleViewVM relatedParksVM;

    [ObservableProperty]
    public CollapsibleViewVM bodyTextVM;

    [ObservableProperty]
    public CollapsibleViewVM tagsVM;

    [ObservableProperty]
    public CollapsibleViewVM organizationsVM;

    [ObservableProperty]
    public CollapsibleViewVM quickFactsVM;

    [ObservableProperty]
    public CollapsibleViewVM amenitiesVM;

    [ObservableProperty]
    public CollapsibleViewVM multimediaVM;

    public PlaceDetailVM(DataService dataService, IMap map)
    {
        Title = "Place";
        this.map = map;
        this.dataService = dataService;

        RelatedParksVM = new CollapsibleViewVM("Related Parks", false);
        BodyTextVM = new CollapsibleViewVM("Full Description", false);
        TagsVM = new CollapsibleViewVM("Tags", false);
        OrganizationsVM = new CollapsibleViewVM("Related Organizations", false);
        QuickFactsVM = new CollapsibleViewVM("Quick Facts", false);
        AmenitiesVM = new CollapsibleViewVM("Amenities", false);
        MultimediaVM = new CollapsibleViewVM("Multimedia", false);
    }

    [RelayCommand]
    async Task OpenMap()
    {
        try
        {
            await map.OpenAsync(Place.DLatitude, Place.DLongitude, new MapLaunchOptions
            {
                Name =  Place.Title,
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
            {"Place", Place }
        });
    }

    [RelayCommand]
    async Task GoToParkAsync(Park park)
    {
        if (park == null)
            return;

        ResultParks result = await dataService.GetParkAsync(park.ParkCode);
        if (result.Data.Count == 1)
        {
            park = result.Data[0];
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", "Unable to get park!", "OK");
        }

        await Shell.Current.GoToAsync(nameof(ParkDetailPage), true, new Dictionary<string, object>
        {
            {"Park", park }
        });
    }
}
