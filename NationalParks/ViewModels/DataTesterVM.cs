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
        //await GetAllPlaces();
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

    //async Task GetAllPlaces()
    //{
    //    try
    //    {
    //        IsBusy = true;
    //        ResultPlaces result;
    //        string states = "";

    //        if (Filter is not null)
    //        {
    //            // Apply any filters prior to getting the items
    //            foreach (var state in Filter.States)
    //            {
    //                if (states.Length > 0)
    //                {
    //                    states += ",";
    //                }
    //                states += state.Abbreviation;
    //            }
    //        }

    //        while (totalItems > startItems)
    //        {
    //            result = await DataService.GetPlacesAsync(startItems, limitItems, states);
    //            startItems += result.Data.Count;
    //            foreach (var place in result.Data)
    //                Places.Add(place);
    //            totalItems = result.Total;
    //            IsPopulated = true;
    //            TotalCount = totalItems;
    //            CurrentCount = Places.Count;
    //            //ManagedByOrgCount = Places.Where(p => !String.IsNullOrEmpty(p.ManagedByOrg)).Count();
    //            //IsManagedByNpsCount = Places.Where(p => p.IsManagedByNps == 1).Count();
    //            //IsOpenToPublicCount = Places.Where(p => p.IsOpenToPublic == 1).Count();
    //            //IsMapPinHiddenCount = Places.Where(p => p.IsMapPinHidden == 1).Count();
    //            if (!okToContinue)
    //            {
    //                break;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        await Shell.Current.DisplayAlert("Error!", $"{ex.Source}: {ex.Message}", "OK");
    //    }
    //    finally
    //    {
    //        IsBusy = false;
    //    }
    //}

    //async Task GetTours()
    //{
    //    try
    //    {
    //        IsBusy = true;
    //        ResultTours result;
    //        string states = "";

    //        while (totalItems > startItems)
    //        {
    //            result = await DataService.GetToursAsync(startItems, limitItems, states);
    //            startItems += result.Data.Count;
    //            foreach (var tour in result.Data)
    //                Tours.Add(tour);
    //            totalItems = result.Total;
    //            IsPopulated = true;
    //            TotalCount = totalItems;
    //            CurrentCount = Places.Count;
    //            MatchCount = Places.Where(i => i.Images.Count == 0).Count();
    //            //ManagedByOrgCount = Places.Where(i => !String.IsNullOrEmpty(p.ManagedByOrg)).Count();
    //            //IsManagedByNpsCount = Places.Where(i => i.IsManagedByNps == 1).Count();
    //            //IsOpenToPublicCount = Places.Where(i => i.IsOpenToPublic == 1).Count();
    //            //IsMapPinHiddenCount = Places.Where(i => i.IsMapPinHidden == 1).Count();
    //            if (!okToContinue)
    //            {
    //                break;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        await Shell.Current.DisplayAlert("Error!", $"{ex.Source}: {ex.Message}", "OK");
    //    }
    //    finally
    //    {
    //        IsBusy = false;
    //    }
    //}
}
