using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ListVM : BaseVM
{
    public FilterVM Filter { get; set; }

    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    protected int LimitItems = 20;
    protected int TotalItems = 0;

    protected string StatesFilter = "";
    protected string TopicsFilter = "";
    protected string ActivitiesFilter = "";

    [ObservableProperty] public ObservableCollection<BaseModel> items = new();
    [ObservableProperty] int itemsRefreshThreshold = -1;
    [ObservableProperty] double progressClosest;
    [ObservableProperty] string progressText;
    [ObservableProperty] bool isFindingClosest;
    [ObservableProperty] string term;
    [ObservableProperty] string filterName;

    private string baseTitle;
    protected string BaseTitle
    {
        get => baseTitle;
        set
        {
            baseTitle = value;
            ProgressText = $"Retrieving all {BaseTitle}...";
        }
    }

    private bool isPopulated = false;
    public bool IsPopulated
    {
        get => isPopulated;
        set
        {
            if (value == true)
            {
                ItemsRefreshThreshold = 2;
                Title = GetTitle();
            }
            else
            {
                ItemsRefreshThreshold = -1;
            }
            isPopulated = value;
        }
    }

    public ListVM(IConnectivity connectivity, IGeolocation geolocation)
    {
        IsBusy = false;
        this.geolocation = geolocation;
        this.connectivity = connectivity;
    }

    [RelayCommand]
    public async Task GoToFilter(string pageType)
    {
        await Shell.Current.GoToAsync($"{pageType}FilterPage", true, new Dictionary<string, object>
        {
            {"VM", this }
        });
    }

    [RelayCommand]
    public async Task GoToDetail(BaseModel model)
    {
        if (model == null)
            return;

        await Shell.Current.GoToAsync($"{model.GetType().Name}DetailPage", true, new Dictionary<string, object>
        {
            {"Model", model}
        });
    }

    [RelayCommand]
    public void CancelGetClosest()
    {
        IsFindingClosest = false;
    }

    [RelayCommand]
    public async Task<Result> GetItems()
    {
        if (IsBusy)
            return null;

        Result result = new();

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return null;
            }

            IsBusy = true;

            GetFilterSelections();

            // Populate the list
            result = await DataService.GetItemsAsync(Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter);
            TotalItems = result.Total;
            switch (term)
            {
                case ResultParks.Term:
                    ResultParks resultParks = (ResultParks)result;
                    foreach (var item in resultParks.Data)
                        Items.Add(item);
                    break;
                case ResultCampgrounds.Term:
                    ResultCampgrounds resultCampgrounds = (ResultCampgrounds)result;
                    foreach (var item in resultCampgrounds.Data)
                        Items.Add(item);
                    break;
                case ResultPlaces.Term:
                    ResultPlaces resultPlaces = (ResultPlaces)result;
                    foreach (var item in resultPlaces.Data)
                    {
                        // This code addresses the condition where Place has no location but
                        // but it has at least one related park
                        if (item.DLatitude < 0 && item.RelatedParks.Count > 0)
                        {
                            ResultParks resultPark = await DataService.GetParkForParkCodeAsync(item.RelatedParks[0].ParkCode);
                            if (resultPark.Data.Count == 1)
                            {
                                var park = resultPark.Data[0];
                                item.Latitude = park.Latitude;
                                item.Longitude = park.Longitude;
                            }
                        }
                        Items.Add(item);
                    }
                    break;
                case ResultTours.Term:
                    ResultTours resultTours = (ResultTours)result;
                    foreach (var item in resultTours.Data)
                    {
                        // This addresses the condition that Tours don't have a location but the associated park does
                        ResultParks resultPark = await DataService.GetParkForParkCodeAsync(item.Park.ParkCode);
                        if (resultPark.Data.Count == 1)
                        {
                            var park = resultPark.Data[0];
                            item.Latitude = park.Latitude;
                            item.Longitude = park.Longitude;
                        }
                        Items.Add(item);
                    }
                    break;
                case ResultThingsToDo.Term:
                    ResultThingsToDo resultThingsToDo = (ResultThingsToDo)result;
                    foreach (var item in resultThingsToDo.Data)
                        Items.Add(item);
                    break;
                case ResultWebcams.Term:
                    ResultWebcams resultWebcams = (ResultWebcams)result;
                    foreach (var item in resultWebcams.Data)
                        Items.Add(item);
                    break;
                case ResultEvents.Term:
                    ResultEvents resultEvents = (ResultEvents)result;
                    foreach (var item in resultEvents.Data)
                        Items.Add(item);
                    break;
                default:
                    throw new Exception($"DataService.GetItemsAsync -- No idea what that means: {term}");
            }
            IsPopulated = true;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }

        return result;
    }

    [RelayCommand]
    public async Task GetClosest()
    {
        if (IsBusy)
            return;

        try
        {
            // Get cached location, else get real location.
            var location = await geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            IsFindingClosest = true;
            if (Items.Count < TotalItems)
            {
                // Get the rest of the items
                LimitItems = 50;
                while (TotalItems > Items.Count && IsFindingClosest)
                {
                    ProgressClosest = (double)Items.Count / (double)TotalItems;
                    await GetItems();
                }
                LimitItems = 20;
            }

            if (IsFindingClosest)
            {
                // Find closest item to us
                var first = items.OrderBy(m => location.CalculateDistance(
                    new Location(m.DLatitude, m.DLongitude), DistanceUnits.Miles))
                    .FirstOrDefault();

                await GoToDetail(first);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
        finally
        {
            IsFindingClosest = false;
            IsBusy = false;
        }
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }
    public void ClearData()
    {
        Items.Clear();
        IsPopulated = false;
    }
    protected string GetTitle()
    {
        string tmp = $"{BaseTitle}";
        if (TotalItems > 0)
        {
            tmp += $"  ({TotalItems}";
            if (Filter is not null && Filter.IsFiltered)
            {
                tmp += $", filtered";
            }
            tmp += ")";
        }

        return tmp;
    }
    protected void GetFilterSelections()
    {
        if (Filter is not null)
        {
            StatesFilter = GetStatesFilter(Filter.States);
            TopicsFilter = GetTopicsFilter(Filter.Topics);
            ActivitiesFilter = GetActivitiesFilter(Filter.Activities);
        }
    }
    private static string GetStatesFilter(List<State> states)
    {
        string filter = "";

        foreach (var state in states)
        {
            if (filter.Length > 0)
            {
                filter += ",";
            }
            filter += state.Abbreviation;
        }

        return filter;
    }
    private static string GetTopicsFilter(List<Topic> topics)
    {
        string filter = "";

        foreach (var topic in topics)
        {
            if (filter.Length > 0)
            {
                filter += "%2D";
            }
            filter += topic.Id;
        }

        return filter;
    }
    private static string GetActivitiesFilter(List<Models.Activity> activities)
    {
        string filter = "";

        foreach (var activity in activities)
        {
            if (filter.Length > 0)
            {
                filter += "%2D";
            }
            filter += activity.Id;
        }

        return filter;
    }
}
