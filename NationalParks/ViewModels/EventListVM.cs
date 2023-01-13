using NationalParks.Services;
using System.Text.Json;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class EventListVM : ListVM
{
    // For holding the available filter selections
    private Collection<Models.State> States { get; } = new();

    readonly IConnectivity connectivity;

    public ObservableCollection<Models.Event> Events { get; } = new();

    public EventListVM(IConnectivity connectivity, IGeolocation geolocation) : base(geolocation)
    {
        IsBusy = false;
        BaseTitle = "Events";
        this.connectivity = connectivity;
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    public void ClearData()
    {
        Events.Clear();
        StartItems = 0;
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
            ResultEvents result = await GetEventsData(StartItems, LimitItems, StatesFilter);

            StartItems += result.Data.Count;
            TotalItems = result.Total;
            Title = $"Events ({TotalItems})";
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
    private async Task<ResultEvents> GetEventsData(int startItems, int limitItems, string statesFilter)
    {
        ResultEvents result = await DataService.GetEventsAsync(startItems, limitItems, statesFilter);
        foreach (var npsEvent in result.Data)
            Events.Add(npsEvent);

        return result;
    }
}
