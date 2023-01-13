using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class WebcamListVM : ListVM
{
    readonly IConnectivity connectivity;

    private int startWebcams = 0;
    public ObservableCollection<Models.Webcam> Webcams { get; } = new();

    public WebcamListVM(IConnectivity connectivity, IGeolocation geolocation) : base(geolocation)
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

            ResultWebcams result = await DataService.GetWebcamsAsync(startWebcams);
            foreach (var webcam in result.Data)
                Webcams.Add(webcam);
            startWebcams += result.Data.Count;
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
