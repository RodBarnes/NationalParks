using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels;

public partial class ListVM : BaseVM
{
    protected readonly IConnectivity connectivity;
    protected readonly IGeolocation geolocation;

    protected int LimitItems = 20;
    protected int TotalItems = 0;

    [ObservableProperty] ObservableCollection<BaseModel> items = new();
    [ObservableProperty] int itemsRefreshThreshold = -1;
    [ObservableProperty] double progressClosest;
    [ObservableProperty] string progressText;
    [ObservableProperty] bool showClosestProgress;
    [ObservableProperty] string term;

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
        ShowClosestProgress = false;
    }

    protected async Task GetClosest()
    {
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
            var first = Items.OrderBy(m => location.CalculateDistance(
                new Location(m.DLatitude, m.DLongitude), DistanceUnits.Miles))
                .FirstOrDefault();

            await GoToDetail(first);
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            await Shell.Current.DisplayAlert("Error!", $"{this.GetType()}.GetClosest: {msg}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
    protected async Task<T> GetItems<T>(string term)
    {
        T result = default;

        try
        {
            IsBusy = true;
            await BuildFilterSelections();

            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return result;
            }

            result = await DataService.GetItemsAsync<T>(term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            await Shell.Current.DisplayAlert("Error!", $"{this.GetType()}.GetItems: {msg}", "OK");
        }
        finally
        {
            IsBusy = false;
        }

        return result;
    }
    protected async Task PopulateData()
    {
        Title = GetTitle();

        // Populate the available selections
        await ReadStates();
        await GetAllActivitiesAsync();
        await GetAllTopicsAsync();
    }
    protected async Task BuildFilterSelections()
    {
        StatesFilter = GetStatesFilter(SelectedStates);
        TopicsFilter = await GetTopicsFilter(SelectedTopics);
        ActivitiesFilter = await GetActivitiesFilter(SelectedActivities);
    }
    private void ClearData()
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
            if (IsFiltered)
            {
                tmp += $", filtered";
            }
            tmp += ")";
        }

        return tmp;
    }
    protected static async Task FillLocationFromPark(BaseModel item, string parkCode)
    {
        try
        {
            ResultParks resultPark = await DataService.GetItemsForParkCodeAsync<ResultParks>(ResultParks.Term, parkCode);
            if (resultPark.Data.Count > 0)
            {
                var park = resultPark.Data.First();
                item.Latitude = park.Latitude;
                item.Longitude = park.Longitude;
            }
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            await Shell.Current.DisplayAlert("Error!", $"ListVM.FillLocationFromPark: {msg}", "OK");
        }
    }

    #region Filter

    // Filters created from the selections
    protected string StatesFilter;
    protected string TopicsFilter;
    protected string ActivitiesFilter;

    // Whether to hide/show each filter section
    [ObservableProperty] string filterName;
    [ObservableProperty] bool allowFilterStates;
    [ObservableProperty] bool allowFilterTopics;
    [ObservableProperty] bool allowFilterActivities;
    [ObservableProperty] string queryFilter;

    // Available selections
    public static ObservableCollection<State> StateSelections { get; } = new();
    public static ObservableCollection<Topic> TopicSelections { get; } = new();
    public static ObservableCollection<Models.Activity> ActivitySelections { get; } = new();

    // Selected values
    public ObservableCollection<object> SelectedTopics { get; set; } = new();
    public ObservableCollection<object> SelectedActivities { get; set; } = new();
    public ObservableCollection<object> SelectedStates { get; set; } = new();

    bool IsFiltered => (
                SelectedStates.Count > 0 ||
                SelectedTopics.Count > 0 ||
                SelectedActivities.Count > 0);

    protected static string GetStatesFilter(ICollection<object> states)
    {
        if (states.Count == 0)
            return "";

        string filter = "";

        foreach (State state in states)
        {
            if (filter.Length > 0)
            {
                filter += ",";
            }
            filter += state.Abbreviation;
        }

        return filter;
    }
    protected static async Task<string> GetTopicsFilter(ICollection<object> topics)
    {
        if (topics.Count == 0)
            return "";

        string filter = "";
        string idList = "";

        // Build the list of Ids for the selected topics
        foreach (Models.Topic topic in topics)
        {
            if (idList.Length > 0)
            {
                idList += ",";
            }
            idList += topic.Id;
        }

        try
        {
            // Get the list of related parks for the Ids
            ResultTopics result = await DataService.GetItemsForIdsAsync<ResultTopics>(ResultTopics.Term, idList);
            foreach (Topic topic in result.Data)
            {
                foreach (RelatedPark park in topic.Parks)
                {
                    if (filter.Length > 0)
                    {
                        filter += ",";
                    }
                    filter += park.ParkCode;
                }
            }
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            await Shell.Current.DisplayAlert("Error!", $"ListVM.GetTopicsFilter: {msg}", "OK");
        }

        return filter;
    }
    protected static async Task<string> GetActivitiesFilter(ICollection<object> activities)
    {
        if (activities.Count == 0)
            return "";

        string filter = "";
        string idList = "";

        // Build the list of Ids for the selected Activities
        foreach (Models.Activity activity in activities)
        {
            if (idList.Length > 0)
            {
                idList += ",";
            }
            idList += activity.Id;
        }

        try
        {
            // Get the list of related parks for the Ids
            ResultActivities result = await DataService.GetItemsForIdsAsync<ResultActivities>(ResultActivities.Term, idList);
            foreach (Models.Activity activity in result.Data)
            {
                foreach (RelatedPark park in activity.Parks)
                {
                    if (filter.Length > 0)
                    {
                        filter += ",";
                    }
                    filter += park.ParkCode;
                }
            }
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            await Shell.Current.DisplayAlert("Error!", $"ListVM.GetActivitiesFilter: {msg}", "OK");
        }

        return filter;
    }
    protected static async Task GetAllTopicsAsync()
    {
        if (TopicSelections?.Count > 0)
            return;

        try
        {
            int startTopics = 0;
            int totalTopics = 1;

            while (totalTopics > startTopics)
            {
                ResultTopics resultTopics = await DataService.GetItemsAsync<ResultTopics>(ResultTopics.Term, startTopics);
                totalTopics = resultTopics.Total;
                startTopics += resultTopics.Data.Count;
                foreach (var topic in resultTopics.Data)
                    TopicSelections.Add(topic);
            }
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            await Shell.Current.DisplayAlert("Error!", $"ListVM.GetAllTopicsAsync: {msg}", "OK");
        }
    }
    protected static async Task GetAllActivitiesAsync()
    {
        if (ActivitySelections?.Count > 0)
            return;

        try
        {
            int startActivities = 0;
            int totalActivities = 1;

            while (totalActivities > startActivities)
            {
                ResultActivities resultActivities = await DataService.GetItemsAsync<ResultActivities>(ResultActivities.Term, startActivities);
                totalActivities = resultActivities.Total;
                startActivities += resultActivities.Data.Count;
                foreach (var activity in resultActivities.Data)
                    ActivitySelections.Add(activity);
            }
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            await Shell.Current.DisplayAlert("Error!", $"ListVM.GetAllActivitiesAsync: {msg}", "OK");
        }
    }
    protected static async Task ReadStates()
    {
        if (StateSelections?.Count > 0)
            return;

        using var stream = await FileSystem.OpenAppPackageFileAsync("states_titlecase.json");
        var result = JsonSerializer.Deserialize<ResultStates>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        if (result != null)
        {
            foreach (var item in result.Data)
            {
                StateSelections.Add(item);
            }
        }
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
    public async Task ApplyFilter()
    {
        // Clear the list
        ClearData();
        await Shell.Current.GoToAsync("..", true);
    }

    [RelayCommand]
    public void ClearFilter()
    {
        // Clear the selections
        SelectedTopics.Clear();
        SelectedActivities.Clear();
        SelectedStates.Clear();
        QueryFilter = "";

        Shell.Current.DisplayAlert("Filter", "All filter values have been cleared.", "OK");
    }

    #endregion
}
