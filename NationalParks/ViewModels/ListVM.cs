using Microsoft.Maui.Devices.Sensors;

namespace NationalParks.ViewModels;

public partial class ListVM : BaseVM
{
    public FilterVM Filter { get; set; }

    readonly IGeolocation geolocation;

    protected readonly int LimitItems = 20;
    protected int StartItems = 0;
    protected int TotalItems = 0;

    protected string StatesFilter = "";
    protected string TopicsFilter = "";
    protected string ActivitiesFilter = "";
    protected string BaseTitle = "";

    [ObservableProperty] int itemsRefreshThreshold = -1;

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
                StartItems = 0;
            }
            isPopulated = value;
        }
    }

    public ListVM(IGeolocation geolocation)
    {
        this.geolocation = geolocation;
    }

    [RelayCommand]
    async Task GoToFilter(string pageName)
    {
        await Shell.Current.GoToAsync(pageName, true, new Dictionary<string, object>
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

    [RelayCommand]
    async Task GetClosest(IEnumerable<BaseModel> items)
    {
        //if (IsBusy || items.Count == 0)
        //    return;

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
            StatesFilter = GetSelectedStates(Filter.States);
            TopicsFilter = GetSelectedTopics(Filter.Topics);
            ActivitiesFilter = GetSelectedActivities(Filter.Activities);
        }
    }
    private string GetSelectedStates(List<State> states)
    {
        string filter = "";

        foreach (var state in Filter.States)
        {
            if (filter.Length > 0)
            {
                filter += ",";
            }
            filter += state.Abbreviation;
        }

        return filter;
    }
    private string GetSelectedTopics(List<Topic> topics)
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
    private string GetSelectedActivities(List<Models.Activity> activities)
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
