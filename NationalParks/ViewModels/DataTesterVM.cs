using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class DataTesterVM : BaseVM
{
    DataService dataService;
    IConnectivity connectivity;

    private int startItems = 0;
    private int limitItems = 100;
    private int totalItems = 1;

    private bool okToContinue = false;

    public ObservableCollection<Models.Place> Places { get; } = new();

    public FilterVM Filter { get; set; }

    [ObservableProperty]
    bool isPopulated = false;

    [ObservableProperty]
    string currentState;

    public DataTesterVM(DataService dataService, IConnectivity connectivity)
    {
        Title = "Tester";
        this.dataService = dataService;
        this.connectivity = connectivity;

        currentState = "Waiting...";
    }

    [RelayCommand]
    async Task StopAction()
    {
        okToContinue = false;
        currentState = "Stopped";
    }

    [RelayCommand]
    async Task StartActionAsync()
    {
        okToContinue = true;
        currentState = "Running...";
        await GetAllPlacesAsync();
    }

    [RelayCommand]
    async Task ClearDataAsync()
    {
        Places.Clear();
        IsPopulated = false;
        startItems = 0;
        Title = "Places";
        currentState = "Cleared";
    }

    async Task GetAllPlacesAsync()
    {
        try
        {
            IsBusy = true;
            ResultPlaces result;
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

            //using var stream = await FileSystem.OpenAppPackageFileAsync("places_MO.json");
            //result = System.Text.Json.JsonSerializer.Deserialize<ResultPlaces>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //foreach (var place in result.Data)
            //    Places.Add(place);

            while (totalItems > startItems)
            {
                result = await dataService.GetPlacesAsync(startItems, limitItems, states);
                startItems += result.Data.Count;
                foreach (var place in result.Data)
                    Places.Add(place);
                if (!int.TryParse(result.Total, out totalItems))
                {
                    totalItems = 0;
                }
                IsPopulated = true;
                Title = $"Places ({Places.Count} of {totalItems})";
                if (!okToContinue)
                {
                    break;
                }
            }
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
