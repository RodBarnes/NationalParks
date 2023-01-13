using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class CampgroundListVM : ListVM
{
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    public ObservableCollection<Models.Campground> Campgrounds { get; } = new();

    public CampgroundListVM(IConnectivity connectivity, IGeolocation geolocation)
    {
        IsBusy = false;
        BaseTitle = "Campgrounds";
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    public void ClearData()
    {
        Campgrounds.Clear();
        IsPopulated = false;
    }

    [RelayCommand]
    async Task GoToDetail(Campground campground)
    {
        if (campground == null)
            return;

        await Shell.Current.GoToAsync(nameof(CampgroundDetailPage), true, new Dictionary<string, object>
        {
            {"Model", campground}
        });
    }

    [RelayCommand]
    async Task GetClosest()
    {
        if (IsBusy || Campgrounds.Count == 0)
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
            var first = Campgrounds.OrderBy(m => location.CalculateDistance(
                new Location(m.DLatitude, m.DLongitude), DistanceUnits.Miles))
                .FirstOrDefault();

            await GoToDetail(first);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
    }

    [RelayCommand]
    async Task GetItems()
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
            ResultCampgrounds result;
            string states = "";

            if (Filter is not null)
            {
                // Apply any filters prior to getting the items
                foreach (var state in Filter.States)
                {
                    if (states.Length > 0)
                    {
                        states += ",";
                    }
                    states += state.Abbreviation;
                }
            }

            result = await DataService.GetCampgroundsAsync(startItems, limitItems, states);
            startItems += result.Data.Count;
            foreach (var campground in result.Data)
                Campgrounds.Add(campground);
            totalItems = result.Total;
            IsPopulated = true;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
