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
        startItems = 0;
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
            ResultEvents result;
            string states = "";

            // Apply any filters prior to getting the items
            foreach (var state in Filter.States)
            {
                if (states.Length > 0)
                {
                    states += ",";
                }
                states += state.Abbreviation;
            }

            result = await DataService.GetEventsAsync(startItems, limitItems, states);
            startItems += result.Data.Count;
            foreach (var npsEvent in result.Data)
                Events.Add(npsEvent);

            totalItems = result.Total;
            Title = $"Events ({totalItems})";
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
