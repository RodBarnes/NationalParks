using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class PlaceListVM : ListVM
{
    readonly IConnectivity connectivity;

    [ObservableProperty] public ObservableCollection<Models.BaseModel> items;

    public PlaceListVM(IConnectivity connectivity, IGeolocation geolocation) : base(geolocation)
    {
        IsBusy = false;
        BaseTitle = "Places";
        this.connectivity = connectivity;
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    public void ClearData()
    {
        Items.Clear();
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
            ResultPlaces result = await DataService.GetPlacesAsync(StartItems, LimitItems, StatesFilter);

            Items = new(result.Data);
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
}
