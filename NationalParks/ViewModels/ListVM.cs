using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels;

public partial class ListVM : BaseVM
{
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    protected int LimitItems = 20;
    protected int TotalItems = 0;

    [ObservableProperty] ObservableCollection<BaseModel> items = new();
    [ObservableProperty] int itemsRefreshThreshold = -1;
    [ObservableProperty] double progressClosest;
    [ObservableProperty] string progressText;
    [ObservableProperty] bool isFindingClosest;
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
    public async Task PopulateData()
    {
        Title = GetTitle();
        await GetItems();

        // Populate the available selections
        await ReadStates();
        await GetAllActivitiesAsync();
        await GetAllTopicsAsync();
    }

    [RelayCommand]
    public async Task GetItems()
    {
        if (IsBusy)
            return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            IsBusy = true;
            await BuildFilterSelections();

            // Populate the list
            switch (Term)
            {
                case ResultParks.Term:
                    ResultParks resultParks = await DataService.GetItemsAsync<ResultParks>(ResultParks.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (Park item in resultParks.Data)
                    {
                        item.FillMainImage();
                        Items.Add(item);
                    }
                    TotalItems = resultParks.Total;
                    break;
                case ResultCampgrounds.Term:
                    ResultCampgrounds resultCampgrounds = await DataService.GetItemsAsync<ResultCampgrounds>(ResultCampgrounds.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (Campground item in resultCampgrounds.Data)
                    {
                        item.FillMainImage();
                        Items.Add(item);
                    }
                    TotalItems = resultCampgrounds.Total;
                    break;
                case ResultPlaces.Term:
                    ResultPlaces resultPlaces = await DataService.GetItemsAsync<ResultPlaces>(ResultPlaces.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (Place item in resultPlaces.Data)
                    {
                        if (item.DLatitude < 0 && item.RelatedParks.Count > 0)
                        {
                            // Place is missing location so use first related park location
                            string parkCode = item.RelatedParks.First().ParkCode;
                            await FillLocationFromPark(item, parkCode);
                        }
                        item.FillMainImage();
                        Items.Add(item);
                    }
                    TotalItems = resultPlaces.Total;
                    break;
                case ResultTours.Term:
                    ResultTours resultTours = await DataService.GetItemsAsync<ResultTours>(ResultTours.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (Tour item in resultTours.Data)
                    {
                        if (item.DLatitude < 0)
                        {
                            // Tour is missing location so use park location
                            string parkCode = item.Park.ParkCode;
                            await FillLocationFromPark(item, parkCode);
                        }
                        item.FillMainImage();
                        Items.Add(item);
                    }
                    TotalItems = resultTours.Total;
                    break;
                case ResultNewsReleases.Term:

                    // The commendted code (below) was being used to investigate why NewsRelease would
                    // not display the images in the Release version.

                    //var fileName = @"E:\nps_log (release, MainImage.Id).txt";
                    //var stream = new StreamWriter(fileName);

                    ResultNewsReleases resultReleases = await DataService.GetItemsAsync<ResultNewsReleases>(ResultNewsReleases.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (NewsRelease item in resultReleases.Data)
                    {
                        if (item.DLatitude < 0)
                        {
                            // News Release is missing location so use park location
                            string parkCode = item.ParkCode;
                            await FillLocationFromPark(item, parkCode);
                        }
                        item.FillMainImage();
                        Items.Add(item);

                        //var output = "";
                        //if (ImageSource.IsNullOrEmpty(item.MainImage))
                        //{
                        //    output = "MainImage is null or empty";
                        //}
                        //else if (item.MainImage.Id == Guid.Empty)
                        //{
                        //    output = "MainImage.Id is Empty";
                        //}
                        //else
                        //{
                        //    output = item.MainImage.Id.ToString();
                        //}
                        //stream.Write($"{item.Title} [{output}]\n");
                        //item.Description = output;
                    }
                    //stream.Close();

                    TotalItems = resultReleases.Total;
                    break;
                case ResultThingsToDo.Term:
                    ResultThingsToDo resultThingsToDo = await DataService.GetItemsAsync<ResultThingsToDo>(ResultThingsToDo.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (ThingToDo item in resultThingsToDo.Data)
                    {
                        item.FillMainImage();
                        Items.Add(item);
                    }
                    TotalItems = resultThingsToDo.Total;
                    break;
                case ResultWebcams.Term:
                    ResultWebcams resultWebcams = await DataService.GetItemsAsync<ResultWebcams>(ResultWebcams.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (Webcam item in resultWebcams.Data)
                    {
                        item.FillMainImage();
                        Items.Add(item);
                    }
                    TotalItems = resultWebcams.Total;
                    break;
                case ResultEvents.Term:
                    ResultEvents resultEvents = await DataService.GetItemsAsync<ResultEvents>(ResultEvents.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (Event item in resultEvents.Data)
                    {
                        item.FillMainImage();
                        Items.Add(item);
                    }
                    TotalItems = resultEvents.Total;
                    break;
                case ResultPeople.Term:
                    ResultPeople resultPeople = await DataService.GetItemsAsync<ResultPeople>(ResultPeople.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (Person item in resultPeople.Data)
                    {
                        if (item.DLatitude < 0 && item.RelatedParks.Count > 0)
                        {
                            // Person is missing location so use first related park location
                            string parkCode = item.RelatedParks.First().ParkCode;
                            await FillLocationFromPark(item, parkCode);
                        }
                        item.FillMainImage();
                        Items.Add(item);
                    }
                    TotalItems = resultPeople.Total;
                    break;
                case ResultArticles.Term:
                    ResultArticles resultArticles = await DataService.GetItemsAsync<ResultArticles>(ResultArticles.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (Article item in resultArticles.Data)
                    {
                        item.FillMainImage();
                        Items.Add(item);
                    }
                    TotalItems = resultArticles.Total;
                    break;
                case ResultVideos.Term:
                    ResultVideos resultVideos = await DataService.GetItemsAsync<ResultVideos>(ResultVideos.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (Multimedia item in resultVideos.Data)
                    {
                        item.FillMainImage();
                        Items.Add(item);
                    }
                    TotalItems = resultVideos.Total;
                    break;
                case ResultAudios.Term:
                    ResultAudios resultAudios = await DataService.GetItemsAsync<ResultAudios>(ResultAudios.Term, Items.Count, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter, QueryFilter);
                    foreach (Multimedia item in resultAudios.Data)
                    {
                        item.FillMainImage();
                        Items.Add(item);
                    }
                    TotalItems = resultAudios.Total;
                    break;
                default:
                    throw new Exception($"{this.GetType()}.GetItems -- No idea what that means: {Term}");
            }
            IsPopulated = true;
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
                var first = Items.OrderBy(m => location.CalculateDistance(
                    new Location(m.DLatitude, m.DLongitude), DistanceUnits.Miles))
                    .FirstOrDefault();

                await GoToDetail(first);
            }
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            await Shell.Current.DisplayAlert("Error!", $"{this.GetType()}.GetClosest: {msg}", "OK");
        }
        finally
        {
            IsFindingClosest = false;
            IsBusy = false;
        }
    }

    [RelayCommand]
    public void CancelGetClosest()
    {
        IsFindingClosest = false;
    }

    private async Task BuildFilterSelections()
    {
        StatesFilter = GetStatesFilter(SelectedStates);
        TopicsFilter = await GetTopicsFilter(SelectedTopics);
        ActivitiesFilter = await GetActivitiesFilter(SelectedActivities);
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
            ResultParks resultPark = await DataService.GetParkForParkCodeAsync(parkCode);
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

    static string GetStatesFilter(ICollection<object> states)
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
    static async Task<string> GetTopicsFilter(ICollection<object> topics)
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
            ResultTopics result = await DataService.GetTopicsForIds(idList);
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
    static async Task<string> GetActivitiesFilter(ICollection<object> activities)
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
            ResultActivities result = await DataService.GetActivitiesForIds(idList);
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
    static async Task GetAllTopicsAsync()
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
    static async Task GetAllActivitiesAsync()
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
    static async Task ReadStates()
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
