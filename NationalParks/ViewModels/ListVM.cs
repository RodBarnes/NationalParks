using Microsoft.Maui.Devices.Sensors;

namespace NationalParks.ViewModels;

public partial class ListVM : BaseVM
{
    readonly IGeolocation geolocation;

    protected readonly int limitItems = 20;
    protected int startItems = 0;
    protected int totalItems = 0;

    public FilterVM Filter { get; set; }

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
                startItems = 0;
            }
            isPopulated = value;
        }
    }
    protected string BaseTitle { get; set; }

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
        if (totalItems > 0)
        {
            tmp += $"  ({totalItems}";
            if (Filter is not null && Filter.IsFiltered)
            {
                tmp += $", filtered";
            }
            tmp += ")";
        }

        return tmp;
    }
}
