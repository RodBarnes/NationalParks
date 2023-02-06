using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Model")]
public partial class ParkDetailVM : DetailVM
{
    [ObservableProperty] Park park;
    [ObservableProperty] CollapsibleViewVM alertsVM;
    [ObservableProperty] CollapsibleViewVM combinedFeesVM;
    [ObservableProperty] CollapsibleViewVM operatingHoursVM;
    [ObservableProperty] CollapsibleViewVM contactsVM;
    [ObservableProperty] CollapsibleViewVM directionsVM;
    [ObservableProperty] CollapsibleListVM topicsVM;
    [ObservableProperty] CollapsibleListVM activitiesVM;
    [ObservableProperty] CollapsibleTextVM weatherVM;
    [ObservableProperty] bool hasAlerts;

    public ParkDetailVM(IMap map) : base(map)
    {
        Title = "Park";
        AlertsVM = new CollapsibleViewVM("Alerts", false);
        CombinedFeesVM = new CollapsibleViewVM("Entrance Fees", false);
        OperatingHoursVM = new CollapsibleViewVM("Operating Hours", false);
        ContactsVM = new CollapsibleViewVM("Contacts", false);
        DirectionsVM = new CollapsibleViewVM("Directions", false);
        TopicsVM = new CollapsibleListVM("Topics", false);
        ActivitiesVM = new CollapsibleListVM("Activities", false);
        WeatherVM = new CollapsibleTextVM("Weather", false);
    }

    [RelayCommand]
    public async void PopulateData()
    {
        await GetAlerts(park);
        HasAlerts = park.HasAlerts;

        WeatherVM.HasContent = park.HasWeather;
        WeatherVM.Text = park.WeatherInfo;
        TopicsVM.HasContent = park.HasTopics;
        TopicsVM.Items = park.Topics.ToList<object>();
        ActivitiesVM.HasContent = park.HasActivities;
        ActivitiesVM.Items = park.Activities.ToList<object>();
    }

    static async Task GetAlerts(Park park)
    {
        if (park.Alerts?.Count > 0)
            return;

        try
        {
            int startAlerts = 0;
            int totalAlerts = 1;
            int limitAlerts = 20;

            while (totalAlerts > startAlerts)
            {
                var result = await DataService.GetAlertsForParkCodeAsync(park.ParkCode, startAlerts, limitAlerts);
                totalAlerts = result.Total;
                startAlerts += result.Data.Count;
                foreach (var alert in result.Data)
                    park.Alerts.Add(alert);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
    }
}
