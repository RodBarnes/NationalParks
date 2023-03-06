using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class DataTesterVM : ListVM
{
    DataService dataService;
    IConnectivity connectivity;
    IGeolocation geolocation;

    private int startItems = 0;
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
        if (IsBusy)
            return;

        IsBusy = true;
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
        Items.Clear();
        startItems = 0;
        CurrentState = "Cleared";
        CurrentCount = MatchCount = TotalCount = 0;
    }

    async Task GetAllItems()
    {
        Title = $"Checking {nameof(ResultTours)}";

        try
        {
            while (totalItems > startItems)
            {
                Result result = await DataService.GetItemsAsync(ResultTours.Term, startItems, 20);
                ResultTours resultDerived = (ResultTours)result;
                startItems += resultDerived.Data.Count();
                foreach (var item in resultDerived.Data)
                {
                    //if (item.DLatitude < 0)
                    //{
                    //    // Tour is missing location so use park location
                    //    string parkCode = item.Park.ParkCode;
                    //    await FillLocationFromPark(item, parkCode);
                    //}
                    item.FillMainImage();
                    Items.Add(item);
                }
                totalItems = result.Total;
                IsPopulated = true;
                TotalCount = totalItems;
                MatchCount = Items.Count;
                CurrentCount = startItems;
                if (!okToContinue)
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"DataTesterVM.GetAllItems: {ex.Source}: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
