﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Park")]
public partial class ParkDetailVM : BaseVM
{
    [ObservableProperty]
    Park park;

    public ObservableCollection<Models.Topic> Topics { get; } = new();
    public ObservableCollection<Models.Activity> Activities { get; } = new();
    public ObservableCollection<Models.Fee> EntranceFees { get; } = new();

    [ObservableProperty]
    public CollapsibleViewVM topicsVM;

    [ObservableProperty]
    public CollapsibleViewVM activitiesVM;

    [ObservableProperty]
    public CollapsibleViewVM entranceFeesVM;

    [ObservableProperty]
    public CollapsibleViewVM directionsVM;

    [ObservableProperty]
    public CollapsibleViewVM weatherVM;

    IMap map;

    public ParkDetailVM(IMap map)
    {
        Title = "Park";
        this.map = map;

        TopicsVM = new CollapsibleViewVM("Topics", false);
        ActivitiesVM = new CollapsibleViewVM("Activities", false);
        EntranceFeesVM = new CollapsibleViewVM("Entrance Fees", false);
        DirectionsVM = new CollapsibleViewVM("Directions", false);
        WeatherVM = new CollapsibleViewVM("Weather", false);
    }

    [RelayCommand]
    public void ToggleTopics()
    {
        TopicsVM.Toggle();
    }

    [RelayCommand]
    public void ToggleActivities()
    {
        ActivitiesVM.Toggle();
    }

    [RelayCommand]
    public void ToggleEntranceFees()
    {
        EntranceFeesVM.Toggle();
    }

    [RelayCommand]
    public void ToggleDirections()
    {
        DirectionsVM.Toggle();
    }

    [RelayCommand]
    public void ToggleWeather()
    {
        WeatherVM.Toggle();
    }

    [RelayCommand]
    async Task OpenMap()
    {
        try
        {
            await map.OpenAsync(Park.DLatitude, Park.DLongitude, new MapLaunchOptions
            {
                Name = Park.Name,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to launch maps: {ex.Message}");
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task GoToHours()
    {
        await Shell.Current.GoToAsync(nameof(ParkHoursPage), true, new Dictionary<string, object>
        {
            {"Park", Park }
        });
    }

    [RelayCommand]
    async Task GoToImages()
    {
        await Shell.Current.GoToAsync(nameof(ParkImageListPage), true, new Dictionary<string, object>
        {
            {"Park", Park }
        });
    }

    public void PopulateData()
    {
        foreach (var topic in Park.Topics)
            Topics.Add(topic);

        foreach (var activity in Park.Activities)
            Activities.Add(activity);

        foreach (var entranceFee in Park.EntranceFees)
            EntranceFees.Add(entranceFee);
    }
}
