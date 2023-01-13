using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class WebcamListVM : ListVM
{
    readonly IConnectivity connectivity;

    private int startWebcams = 0;
    public ObservableCollection<Models.Webcam> Webcams { get; } = new();

    public WebcamListVM(IConnectivity connectivity)
    {
        BaseTitle = "Webcams";
        this.connectivity = connectivity;
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    [RelayCommand]
    async Task GoToDetail(Webcam webcam)
    {
        if (webcam == null)
            return;

        await Shell.Current.GoToAsync(nameof(WebcamDetailPage), true, new Dictionary<string, object>
        {
            {"Webcam", webcam }
        });
    }

    [RelayCommand]
    async Task GetClosest()
    {
        await Shell.Current.DisplayAlert("Filter", $"How would GetClosest() work for {this}?", "OK");
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
            ResultWebcams result;

            //using var stream = await FileSystem.OpenAppPackageFileAsync("webcams_0.json");
            //result = System.Text.Json.JsonSerializer.Deserialize<ResultWebcams>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //foreach (var webcam in result.Data)
            //    Webcams.Add(webcam);

            result = await DataService.GetWebcamsAsync(startWebcams);
            startWebcams += result.Data.Count;
            foreach (var webcam in result.Data)
                Webcams.Add(webcam);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
