using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Model")]
public partial class ParkDetailVM : DetailVM
{
    readonly IMap map;
    [ObservableProperty] Park park;
    [ObservableProperty] AlertsVM alerts;
    [ObservableProperty] ParkingLotsVM parkingLots;
    [ObservableProperty] FeesVM fees;
    [ObservableProperty] OperatingHoursVM operatingHours;
    [ObservableProperty] ContactsVM contacts;
    [ObservableProperty] DirectionsVM directions;
    [ObservableProperty] CollapsibleListVM topics;
    [ObservableProperty] CollapsibleListVM activities;
    [ObservableProperty] CollapsibleTextVM weather;

    public ParkDetailVM(IMap map) : base(map)
    {
        this.map = map;
        Title = "Park";
    }

    [RelayCommand]
    public async void PopulateData()
    {
        Model = Park;

        if (Park.Alerts?.Count == 0)
            await GetParkProperties(Park, "alerts");

        if (Park.ParkingLots?.Count == 0)
            await GetParkProperties(Park, "parkinglots");

        Weather = new CollapsibleTextVM("Weather", false, Park.WeatherInfo);

        Topics = new CollapsibleListVM("Topics", false, Park.Topics.ToList<object>());
        Activities = new CollapsibleListVM("Activities", false, Park.Activities.ToList<object>());

        Directions = new DirectionsVM("Directions", false, Park.PhysicalAddress?.ToString(), Park.DirectionsInfo);
        Contacts = new ContactsVM("Contacts", false, Park.Contacts.PhoneNumbers, Park.Contacts.EmailAddresses);
        Alerts = new AlertsVM("Alerts", false, Park.Alerts);
        ParkingLots = new ParkingLotsVM(map, "Parking Lots", false, Park.ParkingLots);
        Fees = new FeesVM("Entrance Fees", false, Park.EntranceFees);
        OperatingHours = new OperatingHoursVM("Operating Hours", false, Park.OperatingHours);
    }

    static async Task GetParkProperties(Park park, string term)
    {
        try
        {
            int startItems = 0;
            int totalItems = 1;
            int limitItems = 20;

            while (totalItems > startItems)
            {
                var result = await DataService.GetPropertiesForParkCodeAsync(term, park.ParkCode, startItems, limitItems);
                totalItems = result.Total;
                switch (term)
                {
                    case ResultAlerts.Term:
                        ResultAlerts resultAlerts = (ResultAlerts)result;
                        startItems += resultAlerts.Data.Count;
                        foreach (var item in resultAlerts.Data)
                            park.Alerts.Add(item);
                        break;
                    case ResultParkingLots.Term:
                        ResultParkingLots resultLots = (ResultParkingLots)result;
                        startItems += resultLots.Data.Count;
                        foreach (var item in resultLots.Data)
                            park.ParkingLots.Add(item);
                        break;
                    default:
                        throw new Exception($"ListVM.GetItems -- No idea what that means: {term}");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error!", $"ParkDetailVM.GetParkProperties: {ex.Source}--{ex.Message}", "OK");
        }
    }
}
