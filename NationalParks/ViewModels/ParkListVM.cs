using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class ParkListVM : ListVM
{
    readonly IConnectivity connectivity;

    [ObservableProperty] public ObservableCollection<Models.BaseModel> items;

    public ParkListVM(IConnectivity connectivity, IGeolocation geolocation) : base(geolocation)
    {
        IsBusy = false;
        BaseTitle = "Parks";
        this.connectivity = connectivity;
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

            GetFilterSelections();

            // Populate the list
            ResultParks result = await DataService.GetParksAsync(StartItems, LimitItems, StatesFilter, TopicsFilter, ActivitiesFilter);

            // Need to do this in the detail
            // await GetAlerts(park);

            Items = new(result.Data);
            StartItems += result.Data.Count;
            TotalItems = result.Total;
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
    }

    static async Task GetAlerts(Park park)
    {
        if (park.Alerts?.Count > 0)
            return;

        try
        {
            int startAlerts = 0;
            int totalAlerts = 1;
            int limitAlerts = 20;

            while (totalAlerts > startAlerts)
            {
                var result = await DataService.GetAlertsForParkCodeAsync(park.ParkCode, startAlerts, limitAlerts);
                totalAlerts = result.Total;
                startAlerts += result.Data.Count;
                foreach (var alert in result.Data)
                    park.Alerts.Add(alert);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
    }
}
