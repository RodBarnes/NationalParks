using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class CampgroundListVM : ListVM
{
    readonly IConnectivity connectivity;

    public ObservableCollection<Models.Campground> Campgrounds { get; } = new();

    public CampgroundListVM(IConnectivity connectivity, IGeolocation geolocation) : base(geolocation)
    {
        IsBusy = false;
        BaseTitle = "Campgrounds";
        this.connectivity = connectivity;
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    public void ClearData()
    {
        Campgrounds.Clear();
        IsPopulated = false;
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

            GetFilterSelections();

            // Populate the list
            ResultCampgrounds result = await GetCampgroundsData(StartItems, LimitItems, StatesFilter);

            StartItems += result.Data.Count;
            TotalItems = result.Total;
            IsPopulated = true;
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

    private async Task<ResultCampgrounds> GetCampgroundsData(int startItems, int limitItems, string statesFilter)
    {
        ResultCampgrounds result = await DataService.GetCampgroundsAsync(startItems, limitItems, statesFilter);
        foreach (var campground in result.Data)
            Campgrounds.Add(campground);
        return result;
    }
}
