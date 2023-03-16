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

        if (Park.Alerts == null)
            Park.Alerts = new();
        if (Park.Alerts.Count == 0)
            await GetParkProperties(Park, "alerts");

        if (Park.ParkingLots == null)
            Park.ParkingLots = new();
        if (Park.ParkingLots.Count == 0)
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
                switch (term)
                {
                    case ResultAlerts.Term:
                        ResultAlerts resultAlerts = await DataService.GetItemsForParkCodeAsync<ResultAlerts>(ResultAlerts.Term, park.ParkCode, startItems, limitItems);
                        foreach (Alert item in resultAlerts.Data)
                            park.Alerts.Add(item);
                        totalItems = resultAlerts.Total;
                        startItems = park.Alerts.Count;
                        break;
                    case ResultParkingLots.Term:
                        ResultParkingLots resultLots = await DataService.GetItemsForParkCodeAsync<ResultParkingLots>(ResultParkingLots.Term, park.ParkCode, startItems, limitItems);
                        startItems += resultLots.Data.Count;
                        foreach (ParkingLot item in resultLots.Data)
                            park.ParkingLots.Add(item);
                        totalItems = resultLots.Total;
                        startItems = park.ParkingLots.Count;
                        break;
                    default:
                        throw new Exception($"ListVM.GetItems -- No idea what that means: {term}");
                }
            }
        }
        catch (Exception ex)
        {
            var msg = Utility.ParseException(ex);
            await Shell.Current.DisplayAlert("Error!", $"{this.GetType()}.{Utility.GetCurrentMethod()}: {msg}", "OK");
        }
    }
}
