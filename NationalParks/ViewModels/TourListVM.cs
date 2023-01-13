using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class TourListVM : ListVM
{
    readonly IConnectivity connectivity;

    public ObservableCollection<Models.Tour> Tours { get; } = new();

    public TourListVM(IConnectivity connectivity, IGeolocation geolocation) : base(geolocation)
    {
        IsBusy = false;
        BaseTitle = "Tours";
        this.connectivity = connectivity;
    }

    public async void PopulateData()
    {
        Title = GetTitle();
        await GetItems();
    }

    public void ClearData()
    {
        Tours.Clear();
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
            string states = "";
            string topics = "";
            string activities = "";

            if (Filter is not null)
            {
                // Apply any filters prior to getting the items
                foreach (var topic in Filter.Topics)
                {
                    if (topics.Length > 0)
                    {
                        topics += "%2D";
                    }
                    topics += topic.Id;
                }

                foreach (var activity in Filter.Activities)
                {
                    if (activities.Length > 0)
                    {
                        activities += "%2D";
                    }
                    activities += activity.Id;
                }

                foreach (var state in Filter.States)
                {
                    if (states.Length > 0)
                    {
                        states += ",";
                    }
                    states += state.Abbreviation;
                }
            }

            Park park;
            ResultTours result = await DataService.GetToursAsync(startItems, limitItems, states);
            startItems += result.Data.Count;
            foreach (var tour in result.Data)
            {
                ResultParks resultPark = await DataService.GetParkForParkCodeAsync(tour.Park.ParkCode);
                if (resultPark.Data.Count == 1)
                {
                    park = resultPark.Data[0];
                    tour.Latitude = park.Latitude;
                    tour.Longitude = park.Longitude;
                }
                Tours.Add(tour);
            }
            totalItems = result.Total;
            IsPopulated = true;
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
