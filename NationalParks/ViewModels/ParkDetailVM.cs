using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Model")]
public partial class ParkDetailVM : DetailVM
{
    [ObservableProperty] Park park;
    [ObservableProperty] AlertsVM alertsVM;
    [ObservableProperty] FeesVM feesVM;
    [ObservableProperty] OperatingHoursVM operatingHoursVM;
    [ObservableProperty] ContactsVM contactsVM;
    [ObservableProperty] DirectionsVM directionsVM;
    [ObservableProperty] CollapsibleListVM topicsVM;
    [ObservableProperty] CollapsibleListVM activitiesVM;
    [ObservableProperty] CollapsibleTextVM weatherVM;
    [ObservableProperty] bool hasAlerts;

    public ParkDetailVM(IMap map) : base(map)
    {
        Title = "Park";
    }

    [RelayCommand]
    public async void PopulateData()
    {
        await GetAlerts(Park);
        HasAlerts = Park.HasAlerts;

        WeatherVM = new CollapsibleTextVM("Weather", false, Park.WeatherInfo);

        TopicsVM = new CollapsibleListVM("Topics", false, Park.Topics.ToList<object>());
        ActivitiesVM = new CollapsibleListVM("Activities", false, Park.Activities.ToList<object>());

        DirectionsVM = new DirectionsVM("Directions", false, Park.PhysicalAddress?.ToString(), Park.DirectionsInfo);
        ContactsVM = new ContactsVM("Contacts", false, Park.Contacts.PhoneNumbers, Park.Contacts.EmailAddresses);
        AlertsVM = new AlertsVM("Alerts", false, Park.Alerts);
        FeesVM = new FeesVM("Entrance Fees", false, Park.EntranceFees);
        OperatingHoursVM = new OperatingHoursVM("Operating Hours", false, Park.OperatingHours);
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
