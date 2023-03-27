using CommunityToolkit.Maui.Alerts;
using NationalParks.Services;
using System.Reflection;

namespace NationalParks.ViewModels;

public partial class DataTesterVM : ListVM
{
    DataService dataService;

    private int startItems = 0;
    private int totalItems = 1;
    private bool okToContinue = false;

    [ObservableProperty] bool isPopulated = false;

    [ObservableProperty] string selectedType;
    [ObservableProperty] string currentState;
    [ObservableProperty] int currentCount;
    [ObservableProperty] int totalCount;
    [ObservableProperty] MessageVM message = new();

    public DataTesterVM(DataService dataService, IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        Title = "Tester";
        this.dataService = dataService;

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
            await Toast.Make("First pick a data type...").Show();
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
        CurrentCount = TotalCount = 0;
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
                switch (SelectedType)
                {
                    case "Parks":
                        ResultParks resultParks = await DataService.GetItemsAsync<ResultParks>(ResultParks.Term, startItems, 500);
                        startItems += resultParks.Data.Count;
                        foreach (Park item in resultParks.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultParks.Total;
                        break;
                    case "Campgrounds":
                        ResultCampgrounds resultCampgrounds = await DataService.GetItemsAsync<ResultCampgrounds>(ResultCampgrounds.Term, startItems, 500);
                        startItems += resultCampgrounds.Data.Count;
                        foreach (Campground item in resultCampgrounds.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultCampgrounds.Total;
                        break;
                    case "Tours":
                        ResultTours resultTours = await DataService.GetItemsAsync<ResultTours>(ResultTours.Term, startItems, 500);
                        startItems += resultTours.Data.Count;
                        foreach (Tour item in resultTours.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultTours.Total;
                        break;
                    case "People":
                        ResultPeople resultPeople = await DataService.GetItemsAsync<ResultPeople>(ResultPeople.Term, startItems, 500);
                        startItems += resultPeople.Data.Count;
                        foreach (Person item in resultPeople.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultPeople.Total;
                        break;
                    case "Places":
                        ResultPlaces resultPlaces = await DataService.GetItemsAsync<ResultPlaces>(ResultPlaces.Term, startItems, 500);
                        startItems += resultPlaces.Data.Count;
                        foreach (Place item in resultPlaces.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultPlaces.Total;
                        break;
                    case "NewsReleases":
                        ResultNewsReleases resultNewsReleases = await DataService.GetItemsAsync<ResultNewsReleases>(ResultNewsReleases.Term, startItems, 500);
                        startItems += resultNewsReleases.Data.Count;
                        foreach (NewsRelease item in resultNewsReleases.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultNewsReleases.Total;
                        break;
                    case "Events":
                        ResultEvents resultEvents = await DataService.GetItemsAsync<ResultEvents>(ResultEvents.Term, startItems, 500);
                        startItems += resultEvents.Data.Count;
                        foreach (Event item in resultEvents.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultEvents.Total;
                        break;
                    case "Articles":
                        ResultArticles resultArticles = await DataService.GetItemsAsync<ResultArticles>(ResultArticles.Term, startItems, 500);
                        startItems += resultArticles.Data.Count;
                        foreach (Article item in resultArticles.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultArticles.Total;
                        break;
                    case "ThingsToDo":
                        ResultThingsToDo resultThingsToDo = await DataService.GetItemsAsync<ResultThingsToDo>(ResultThingsToDo.Term, startItems, 500);
                        startItems += resultThingsToDo.Data.Count;
                        foreach (ThingToDo item in resultThingsToDo.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultThingsToDo.Total;
                        break;
                    case "Videos":
                        ResultVideos resultVideos = await DataService.GetItemsAsync<ResultVideos>(ResultVideos.Term, startItems, 500);
                        startItems += resultVideos.Data.Count;
                        foreach (Multimedia item in resultVideos.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultVideos.Total;
                        break;
                    case "Audio":
                        ResultAudio resultAudio = await DataService.GetItemsAsync<ResultAudio>(ResultAudio.Term, startItems, 500);
                        startItems += resultAudio.Data.Count;
                        foreach (Multimedia item in resultAudio.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultAudio.Total;
                        break;
                    case "Webcam":
                        ResultWebcams resultWebcams = await DataService.GetItemsAsync<ResultWebcams>(ResultWebcams.Term, startItems, 500);
                        startItems += resultWebcams.Data.Count;
                        foreach (Webcam item in resultWebcams.Data)
                        {
                            item.FillMainImage();
                            Items.Add(item);
                        }
                        totalItems = resultWebcams.Total;
                        break;
                    default:
                        break;
                }
                IsPopulated = true;
                TotalCount = totalItems;
                CurrentCount = startItems;
                if (!okToContinue)
                {
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            await Utility.HandleException(ex, new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
        }
        finally
        {
            IsBusy = false;
        }
    }
}
