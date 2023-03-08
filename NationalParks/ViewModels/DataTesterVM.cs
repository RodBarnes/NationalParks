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

    [ObservableProperty] string selectedType;
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
        if (String.IsNullOrEmpty(SelectedType))
        {
            await Shell.Current.DisplayAlert("Error", "First pick a data type...", "OK");
            return;
        }

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

    partial void OnSelectedTypeChanged(string value)
    {
        ClearAllData();
    }

    async Task GetAllItems()
    {
        Title = $"Checking {SelectedType}";

        try
        {
            while (totalItems > startItems)
            {
                Result result = null;

                switch (SelectedType)
                {
                    case "Parks":
                        result = await DataService.GetItemsAsync(ResultParks.Term, startItems, 500);
                        ResultParks resultParks = (ResultParks)result;
                        startItems += resultParks.Data.Count();
                        foreach (var item in resultParks.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    case "Campgrounds":
                        result = await DataService.GetItemsAsync(ResultCampgrounds.Term, startItems, 500);
                        ResultCampgrounds resultCampgrounds = (ResultCampgrounds)result;
                        startItems += resultCampgrounds.Data.Count();
                        foreach (var item in resultCampgrounds.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    case "Tours":
                        result = await DataService.GetItemsAsync(ResultTours.Term, startItems, 500);
                        ResultTours resultTours = (ResultTours)result;
                        startItems += resultTours.Data.Count();
                        foreach (var item in resultTours.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    case "People":
                        result = await DataService.GetItemsAsync(ResultPeople.Term, startItems, 500);
                        ResultPeople resultPeople = (ResultPeople)result;
                        startItems += resultPeople.Data.Count();
                        foreach (var item in resultPeople.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    case "Places":
                        result = await DataService.GetItemsAsync(ResultPlaces.Term, startItems, 500);
                        ResultPlaces resultPlaces = (ResultPlaces)result;
                        startItems += resultPlaces.Data.Count();
                        foreach (var item in resultPlaces.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    case "NewsReleases":
                        result = await DataService.GetItemsAsync(ResultNewsReleases.Term, startItems, 500);
                        ResultNewsReleases resultNewsReleases = (ResultNewsReleases)result;
                        startItems += resultNewsReleases.Data.Count();
                        foreach (var item in resultNewsReleases.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    case "Events":
                        result = await DataService.GetItemsAsync(ResultEvents.Term, startItems, 500);
                        ResultEvents resultEvents = (ResultEvents)result;
                        startItems += resultEvents.Data.Count();
                        foreach (var item in resultEvents.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    case "Articles":
                        result = await DataService.GetItemsAsync(ResultArticles.Term, startItems, 500);
                        ResultArticles resultArticles = (ResultArticles)result;
                        startItems += resultArticles.Data.Count();
                        foreach (var item in resultArticles.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    case "ThingsToDo":
                        result = await DataService.GetItemsAsync(ResultThingsToDo.Term, startItems, 500);
                        ResultThingsToDo resultThingsToDo = (ResultThingsToDo)result;
                        startItems += resultThingsToDo.Data.Count();
                        foreach (var item in resultThingsToDo.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    case "Videos":
                        result = await DataService.GetItemsAsync(ResultVideos.Term, startItems, 500);
                        ResultVideos resultVideos = (ResultVideos)result;
                        startItems += resultVideos.Data.Count();
                        foreach (var item in resultVideos.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    case "Audios":
                        result = await DataService.GetItemsAsync(ResultAudios.Term, startItems, 500);
                        ResultAudios resultAudios = (ResultAudios)result;
                        startItems += resultAudios.Data.Count();
                        foreach (var item in resultAudios.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    case "Webcam":
                        result = await DataService.GetItemsAsync(ResultWebcams.Term, startItems, 500);
                        ResultWebcams resultWebcams = (ResultWebcams)result;
                        startItems += resultWebcams.Data.Count();
                        foreach (var item in resultWebcams.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        break;
                    default:
                        break;
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
            var msg = Utility.ParseException(ex);
            await Shell.Current.DisplayAlert("Error!", $"DataTesterVM.GetAllItems: {msg}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
