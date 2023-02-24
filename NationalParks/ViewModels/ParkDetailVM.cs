using NationalParks.Services;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Model")]
public partial class ParkDetailVM : DetailVM
{
    readonly IMap map;
    [ObservableProperty] Park park;
    [ObservableProperty] AlertsVM alertsVM;
    [ObservableProperty] ParkingLotsVM parkingLotsVM;
    [ObservableProperty] FeesVM feesVM;
    [ObservableProperty] OperatingHoursVM operatingHoursVM;
    [ObservableProperty] ContactsVM contactsVM;
    [ObservableProperty] DirectionsVM directionsVM;
    [ObservableProperty] CollapsibleListVM topicsVM;
    [ObservableProperty] CollapsibleListVM activitiesVM;
    [ObservableProperty] CollapsibleTextVM weatherVM;

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

        WeatherVM = new CollapsibleTextVM("Weather", false, Park.WeatherInfo);

        TopicsVM = new CollapsibleListVM("Topics", false, Park.Topics.ToList<object>());
        ActivitiesVM = new CollapsibleListVM("Activities", false, Park.Activities.ToList<object>());

        DirectionsVM = new DirectionsVM("Directions", false, Park.PhysicalAddress?.ToString(), Park.DirectionsInfo);
        ContactsVM = new ContactsVM("Contacts", false, Park.Contacts.PhoneNumbers, Park.Contacts.EmailAddresses);
        AlertsVM = new AlertsVM("Alerts", false, Park.Alerts);
        ParkingLotsVM = new ParkingLotsVM(map, "Parking Lots", false, Park.ParkingLots);
        FeesVM = new FeesVM("Entrance Fees", false, Park.EntranceFees);
        OperatingHoursVM = new OperatingHoursVM("Operating Hours", false, Park.OperatingHours);
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
            await Shell.Current.DisplayAlert("Error!", $"{ex.Source}--{ex.Message}", "OK");
        }
    }
}
