/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Campground), "Model")]
public partial class CampgroundDetailVM : DetailVM
{
    [ObservableProperty] Campground campground;
    [ObservableProperty] string parkName;
    [ObservableProperty] FeesVM fees;
    [ObservableProperty] OperatingHoursVM operatingHours;
    [ObservableProperty] ContactsVM contacts;
    [ObservableProperty] CampsitesVM campsites;
    [ObservableProperty] AmenitiesVM amenities;
    [ObservableProperty] AccessibilityVM accessibility;
    [ObservableProperty] DirectionsVM directions;
    [ObservableProperty] RelatedMultimediaVM multimedia;
    [ObservableProperty] CollapsibleTextVM weather;
    [ObservableProperty] CollapsibleTextVM reservations;
    [ObservableProperty] CollapsibleTextVM regulations;

    public CampgroundDetailVM(IConnectivity connectivity, IMap map) : base(connectivity, map)
    {
        Title = "Campground";
    }

    [RelayCommand]
    public async Task PopulateData()
    {
        Model = Campground;

        ParkName = await GetNameFromParkCode(Campground.ParkCode);

        Weather = new CollapsibleTextVM("Weather", false, Campground.WeatherOverview);
        Reservations = new CollapsibleTextVM("Reservations", false, Campground.ReservationInfo, Campground.ReservationUrl);
        Regulations = new CollapsibleTextVM("Regulations", false, Campground.RegulationsOverview, Campground.RegulationsUrl);

        Directions = new DirectionsVM("Directions", false, Campground.PhysicalAddress?.ToString(), Campground.DirectionsOverview);
        Contacts = new ContactsVM("Contacts", false, Campground.Contacts.PhoneNumbers, Campground.Contacts.EmailAddresses);
        Fees = new FeesVM("Fees", false, Campground.Fees);
        OperatingHours = new OperatingHoursVM("Operating Hours", false, Campground.OperatingHours);
        Campsites = new CampsitesVM("Campsites", false, Campground);
        Amenities = new AmenitiesVM("Amenities", false, Campground);
        Accessibility = new AccessibilityVM("Accessibility", false, Campground);
        Multimedia = new RelatedMultimediaVM("Multimedia", false, Campground.Multimedia);
    }
}
