using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Place), "Place")]
public partial class PlaceDetailVM : BaseVM
{
    IMap map;
    DataService dataService;

    [ObservableProperty] Place place;

    [ObservableProperty] CollapsibleViewVM relatedParksVM;

    [ObservableProperty] CollapsibleViewVM bodyTextVM;

    [ObservableProperty] CollapsibleViewVM tagsVM;

    [ObservableProperty] CollapsibleViewVM organizationsVM;

    [ObservableProperty] CollapsibleViewVM quickFactsVM;

    [ObservableProperty] CollapsibleViewVM amenitiesVM;

    [ObservableProperty] CollapsibleViewVM multimediaVM;

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
    async Task GoToParkFromRelatedParkAsync(RelatedPark relPark)
    {
        if (relPark == null)
            return;

        Park park;

        ResultParks result = await dataService.GetParkForParkCodeAsync(relPark.ParkCode);
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
