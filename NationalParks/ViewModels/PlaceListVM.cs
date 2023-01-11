﻿using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Filter), "Filter")]
public partial class PlaceListVM : BaseVM
{
    readonly IConnectivity connectivity;
    readonly IGeolocation geolocation;

    readonly string baseTitle = "Places";
    readonly int limitItems = 20;
    int startItems = 0;
    int totalItems = 0;

    public ObservableCollection<Models.Place> Places { get; } = new();

    public FilterVM Filter { get; set; }

    [ObservableProperty] int itemsRefreshThreshold = -1;

    private bool isPopulated = false;
    public bool IsPopulated
    {
        get => isPopulated;
        set
        {
            if (value == true)
            {
                ItemsRefreshThreshold = 2;
                Title = BuildTitle();
            }
            else
            {
                ItemsRefreshThreshold = -1;
                startItems = 0;
            }
            isPopulated = value;
        }
    }

    public PlaceListVM(IConnectivity connectivity, IGeolocation geolocation)
    {
        IsBusy = false;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    public async void PopulateData()
    {
        Title = baseTitle;
        await GetItems();
    }

    public void ClearData()
    {
        Places.Clear();
        IsPopulated = false;
    }

    [RelayCommand]
    async Task GoToDetail(Place place)
    {
        if (place == null)
            return;

        await Shell.Current.GoToAsync(nameof(PlaceDetailPage), true, new Dictionary<string, object>
        {
            {"Place", place}
        });
    }

    [RelayCommand]
    async Task GoToFilter()
    {
        await Shell.Current.GoToAsync(nameof(PlaceFilterPage), true, new Dictionary<string, object>
        {
            {"VM", this }
        });
    }

    [RelayCommand]
    async Task GetClosest()
    {
        if (IsBusy || Places.Count == 0)
            return;

        try
        {
            // Get cached location, else get real location.
            var location = await geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }

            // Find closest item to us
            var first = Places.OrderBy(m => location.CalculateDistance(
                new Location(m.DLatitude, m.DLongitude), DistanceUnits.Miles))
                .FirstOrDefault();

            await GoToDetail(first);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
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
            ResultPlaces result;
            Park park;
            string states = "";

            if (Filter is not null)
            {
                // Apply any filters prior to getting the items
                foreach (var state in Filter.States)
                {
                    if (states.Length > 0)
                    {
                        states += ",";
                    }
                    states += state.Abbreviation;
                }
            }

            //using var stream = await FileSystem.OpenAppPackageFileAsync("places_MO.json");
            //result = System.Text.Json.JsonSerializer.Deserialize<ResultPlaces>(stream, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            //foreach (var place in result.Data)
            //    Places.Add(place);

            result = await DataService.GetPlacesAsync(startItems, limitItems, states);
            startItems += result.Data.Count;
            foreach (var place in result.Data)
            {
                // This code addresses the condition where there is no location of the place
                // but it has at least one related park
                if (place.DLatitude < 0 && place.RelatedParks.Count > 0)
                {
                    ResultParks resultPark = await DataService.GetParkForParkCodeAsync(place.RelatedParks[0].ParkCode);
                    if (resultPark.Data.Count == 1)
                    {
                        park = resultPark.Data[0];
                        place.Latitude = park.Latitude;
                        place.Longitude = park.Longitude;
                    }
                }
                Places.Add(place);
            }
            totalItems = result.Total;
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

    private string BuildTitle()
    {
        string tmp = $"{baseTitle} ({totalItems}";
        if (Filter is not null && Filter.IsFiltered)
        {
            tmp += $", filtered";
        }
        tmp += ")";

        return tmp;
    }
}
