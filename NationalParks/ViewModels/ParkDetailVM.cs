using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Model")]
public partial class ParkDetailVM : DetailVM
{
    [ObservableProperty] Park park;
    [ObservableProperty] CollapsibleViewVM alertsVM;
    [ObservableProperty] CollapsibleViewVM combinedFeesVM;
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
        AlertsVM = new CollapsibleViewVM("Alerts", false);
        CombinedFeesVM = new CollapsibleViewVM("Entrance Fees", false);
        OperatingHoursVM = new OperatingHoursVM("Operating Hours", false);
        ContactsVM = new ContactsVM("Contacts", false);
        DirectionsVM = new DirectionsVM("Directions", false);
        TopicsVM = new CollapsibleListVM("Topics", false);
        ActivitiesVM = new CollapsibleListVM("Activities", false);
        WeatherVM = new CollapsibleTextVM("Weather", false);
    }

    [RelayCommand]
    public async void PopulateData()
    {
        await GetAlerts(Park);
        HasAlerts = Park.HasAlerts;

        OperatingHoursVM.HasContent = Park.HasOperatingHours;
        OperatingHoursVM.OperatingHours = Park.OperatingHours;
        ContactsVM.HasContent = Park.HasContacts;
        ContactsVM.PhoneContacts = Park.Contacts.PhoneNumbers;
        ContactsVM.EmailContacts = Park.Contacts.EmailAddresses;
        DirectionsVM.HasContent = Park.HasDirections;
        DirectionsVM.PhysicalAddress = Park.PhysicalAddress?.ToString();
        DirectionsVM.Directions = Park.DirectionsInfo;
        WeatherVM.HasContent = Park.HasWeather;
        WeatherVM.Text = Park.WeatherInfo;
        TopicsVM.HasContent = Park.HasTopics;
        TopicsVM.Items = Park.Topics.ToList<object>();
        ActivitiesVM.HasContent = Park.HasActivities;
        ActivitiesVM.Items = Park.Activities.ToList<object>();
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
