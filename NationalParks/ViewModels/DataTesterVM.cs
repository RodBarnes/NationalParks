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
    private bool isPopulated = false;

    public DataTesterVM(DataService dataService, IConnectivity connectivity)
    {
        Title = "Tester";
        this.dataService = dataService;
        this.connectivity = connectivity;
    }

    [RelayCommand]
    public void StopAction()
    {
        okToContinue = false;
    }

    [RelayCommand]
    async Task StartActionAsync()
    {
        okToContinue = true;
        await GetAllPlacesAsync();
    }

    [RelayCommand]
    public void ClearData()
    {
        Places.Clear();
        IsPopulated = false;
        startItems = 0;
        Title = "Places";
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
