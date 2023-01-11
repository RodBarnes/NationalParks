using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class DataTesterVM : BaseVM
{
    DataService dataService;
    IConnectivity connectivity;

    private int startItems = 0;
    private int limitItems = 500;
    private int totalItems = 1;
    private bool okToContinue = false;

    public ObservableCollection<Models.Place> Places { get; } = new();
    public ObservableCollection<Models.Tour> Tours { get; } = new();

    public FilterVM Filter { get; set; }

    [ObservableProperty] bool isPopulated = false;

    [ObservableProperty] string currentState;
    [ObservableProperty] int currentCount;
    [ObservableProperty] int totalCount;

    [ObservableProperty] int matchCount;
    //[ObservableProperty] int managedByOrgCount;
    //[ObservableProperty] int isManagedByNpsCount;
    //[ObservableProperty] int isOpenToPublicCount;
    //[ObservableProperty] int isMapPinHiddenCount;
    public DataTesterVM(DataService dataService, IConnectivity connectivity)
    {
        Title = "Tester";
        this.dataService = dataService;
        this.connectivity = connectivity;

        CurrentState = "Waiting...";
    }

    [RelayCommand]
    public void StopAction()
    {
        okToContinue = false;
        CurrentState = "Stopped";
    }

    [RelayCommand]
    async Task StartActionAsync()
    {
        okToContinue = true;
        CurrentState = "Running...";
        await GetAllPlacesAsync();
        if (startItems <= totalItems)
        {
            okToContinue = false;
            CurrentState = "Stopped";
        }
    }

    [RelayCommand]
    public void ClearData()
    {
        Places.Clear();
        IsPopulated = false;
        startItems = 0;
        CurrentState = "Cleared";
        CurrentCount = MatchCount = TotalCount = 0;
    }

    async Task GetAllPlacesAsync()
    {
        try
        {
            IsBusy = true;
            ResultTours result;
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
                result = await dataService.GetToursAsync(startItems, limitItems, states);
                startItems += result.Data.Count;
                foreach (var tour in result.Data)
                    Tours.Add(tour);
                if (!int.TryParse(result.Total, out totalItems))
                {
                    totalItems = 0;
                }
                IsPopulated = true;
                TotalCount = totalItems;
                CurrentCount = Places.Count;
                MatchCount = Places.Where(i => i.Images.Count == 0).Count();
                //ManagedByOrgCount = Places.Where(i => !String.IsNullOrEmpty(p.ManagedByOrg)).Count();
                //IsManagedByNpsCount = Places.Where(i => i.IsManagedByNps == 1).Count();
                //IsOpenToPublicCount = Places.Where(i => i.IsOpenToPublic == 1).Count();
                //IsMapPinHiddenCount = Places.Where(i => i.IsMapPinHidden == 1).Count();
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
