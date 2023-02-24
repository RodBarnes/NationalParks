using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class DataTesterVM : ListVM
{
    DataService dataService;
    IConnectivity connectivity;
    IGeolocation geolocation;

    private int startItems = 0;
    private int limitItems = 500;
    private int totalItems = 1;
    private bool okToContinue = false;

    [ObservableProperty] bool isPopulated = false;

    [ObservableProperty] string currentState;
    [ObservableProperty] int currentCount;
    [ObservableProperty] int totalCount;
    [ObservableProperty] int matchCount;

    public DataTesterVM(DataService dataService, IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        Title = "Tester";
        this.dataService = dataService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;

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
        await GetAllItems();
        if (startItems <= totalItems)
        {
            okToContinue = false;
            CurrentState = "Stopped";
        }
    }

    [RelayCommand]
    public void ClearAllData()
    {
        ClearData();
        startItems = 0;
        CurrentState = "Cleared";
        CurrentCount = MatchCount = TotalCount = 0;
    }

    async Task GetAllItems()
    {
        ResultThingsToDo resultDerived;
        Title = $"Checking {nameof(ResultThingsToDo)}";
        Term = "thingstodo";

        try
        {
            IsBusy = true;

            while (totalItems > startItems)
            {
                Result result = await DataService.GetItemsAsync(Term, startItems, limitItems);
                resultDerived = (ResultThingsToDo)result;
                startItems += resultDerived.Data.Count;
                foreach (var item in resultDerived.Data)
                {
                    if (item.RelatedParks.Count > 1)
                    {
                        Items.Add(item);
                    }
                }
                totalItems = result.Total;
                IsPopulated = true;
                TotalCount = totalItems;
                MatchCount = Items.Count;
                CurrentCount = startItems;
                //ManagedByOrgCount = Places.Where(p => !String.IsNullOrEmpty(p.ManagedByOrg)).Count();
                //IsManagedByNpsCount = Places.Where(p => p.IsManagedByNps == 1).Count();
                //IsOpenToPublicCount = Places.Where(p => p.IsOpenToPublic == 1).Count();
                //IsMapPinHiddenCount = Places.Where(p => p.IsMapPinHidden == 1).Count();
                if (!okToContinue)
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"DataTesterVM: {ex.Source}: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
