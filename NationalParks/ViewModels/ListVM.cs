using NationalParks.Services;

namespace NationalParks.ViewModels;

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
    async Task GoToFilter(string pageType)
    {
        await Shell.Current.GoToAsync($"{pageType}FilterPage", true, new Dictionary<string, object>
        {
            {"VM", this }
        });
    }

    [RelayCommand]
    async Task GoToDetail(BaseModel model)
    {
        if (model == null)
            return;

        await Shell.Current.GoToAsync($"{model.GetType().Name}DetailPage", true, new Dictionary<string, object>
        {
            {"Model", model}
        });
    }

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

            // Find closest item to us
            var first = items.OrderBy(m => location.CalculateDistance(
                new Location(m.DLatitude, m.DLongitude), DistanceUnits.Miles))
                .FirstOrDefault();

            await GoToDetail(first);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
    }

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
