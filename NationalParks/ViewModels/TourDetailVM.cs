﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Tour), "Tour")]
public partial class TourDetailVM : BaseVM
{
    IMap map;

    [ObservableProperty]
    Tour tour;

    [ObservableProperty]
    public CollapsibleViewVM alertsVM;

    [ObservableProperty]
    public CollapsibleViewVM combinedFeesVM;

    [ObservableProperty]
    public CollapsibleViewVM operatingHoursVM;

    [ObservableProperty]
    public CollapsibleViewVM contactsVM;

    [ObservableProperty]
    public CollapsibleViewVM topicsVM;

    [ObservableProperty]
    public CollapsibleViewVM activitiesVM;

    [ObservableProperty]
    public CollapsibleViewVM directionsVM;

    [ObservableProperty]
    public CollapsibleViewVM weatherVM;

    public TourDetailVM(IMap map)
    {
        Title = "Tour";
        this.map = map;

        AlertsVM = new CollapsibleViewVM("Alerts", false);
        CombinedFeesVM = new CollapsibleViewVM("Entrance Fees", false);
        OperatingHoursVM = new CollapsibleViewVM("Operating Hours", false);
        ContactsVM = new CollapsibleViewVM("Contacts", false);
        TopicsVM = new CollapsibleViewVM("Topics", false);
        ActivitiesVM = new CollapsibleViewVM("Activities", false);
        DirectionsVM = new CollapsibleViewVM("Directions", false);
        WeatherVM = new CollapsibleViewVM("Weather", false);
    }

    //[RelayCommand]
    //async Task OpenMap()
    //{
    //    try
    //    {
    //        await map.OpenAsync(Tour.DLatitude, Tour.DLongitude, new MapLaunchOptions
    //        {
    //            Name = Tour.Name,
    //            NavigationMode = NavigationMode.None
    //        });
    //    }
    //    catch (Exception ex)
    //    {
    //        Debug.WriteLine($"Unable to launch maps: {ex.Message}");
    //        await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
    //    }
    //}

    [RelayCommand]
    async Task GoToImages()
    {
        await Shell.Current.GoToAsync(nameof(ParkImageListPage), true, new Dictionary<string, object>
        {
            {"Tour", Tour }
        });
    }
}
