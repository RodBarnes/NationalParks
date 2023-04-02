/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
using Microsoft.Maui.Networking;
using NationalParks.Services;
using System.Reflection;

namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Model")]
public partial class ParkDetailVM : DetailVM
{
    readonly IConnectivity connectivity;
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

    public ParkDetailVM(IConnectivity connectivity, IMap map) : base(connectivity, map)
    {
        this.map = map;
        this.connectivity = connectivity;
        Title = "Park";
    }

    [RelayCommand]
    public async void PopulateData()
    {
        Model = Park;

        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No internet connection!",
                $"Please check that either Mobile Data is enabled or WiFi is connected; then try again.", "OK");
            var msg = $"Connectivity.NetworkAccess=={connectivity.NetworkAccess}";
            await Utility.HandleException(new Exception(msg), new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
        }
        else
        {
            if (Park.Alerts == null)
                Park.Alerts = new();

            if (Park.Alerts.Count == 0)
                await GetParkProperties(Park, Terms.alerts);
            Alerts = new AlertsVM("Alerts", false, Park.Alerts);

            if (Park.ParkingLots == null)
                Park.ParkingLots = new();
            if (Park.ParkingLots.Count == 0)
                await GetParkProperties(Park, Terms.parkinglots);
            ParkingLots = new ParkingLotsVM(map, "Parking Lots", false, Park.ParkingLots);
        }

        Weather = new CollapsibleTextVM("Weather", false, Park.WeatherInfo);

        Topics = new CollapsibleListVM("Topics", false, Park.Topics.ToList<object>());
        Activities = new CollapsibleListVM("Activities", false, Park.Activities.ToList<object>());

        Directions = new DirectionsVM("Directions", false, Park.PhysicalAddress?.ToString(), Park.DirectionsInfo);
        Contacts = new ContactsVM("Contacts", false, Park.Contacts.PhoneNumbers, Park.Contacts.EmailAddresses);
        Fees = new FeesVM("Entrance Fees", false, Park.EntranceFees);
        OperatingHours = new OperatingHoursVM("Operating Hours", false, Park.OperatingHours);
    }

    async Task GetParkProperties(Park park, Terms term)
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
            await Utility.HandleException(ex, new CodeInfo(MethodBase.GetCurrentMethod().DeclaringType));
        }
    }
}
