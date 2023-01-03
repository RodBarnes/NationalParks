namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Park), "Park")]
public partial class ParkDetailVM : BaseVM
{
    [ObservableProperty]
    Park park;

    public ObservableCollection<Models.Topic> Topics { get; } = new();
    public ObservableCollection<Models.Activity> Activities { get; } = new();
    public ObservableCollection<Models.CombinedFee> CombinedFees { get; } = new();
    public ObservableCollection<Models.PhoneContact> PhoneContacts { get; } = new();
    public ObservableCollection<Models.EmailContact> EmailContacts { get; } = new();

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

    IMap map;

    public ParkDetailVM(IMap map)
    {
        Title = "Park";
        this.map = map;

        CombinedFeesVM = new CollapsibleViewVM("Entrance Fees", false);
        OperatingHoursVM = new CollapsibleViewVM("Operating Hours", false);
        ContactsVM = new CollapsibleViewVM("Contacts", false);
        TopicsVM = new CollapsibleViewVM("Topics", false);
        ActivitiesVM = new CollapsibleViewVM("Activities", false);
        DirectionsVM = new CollapsibleViewVM("Directions", false);
        WeatherVM = new CollapsibleViewVM("Weather", false);
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
        {
            var combinedFee = new CombinedFee("Entrance", entranceFee);
            CombinedFees.Add(combinedFee);
        }
        foreach (var entrancePass in Park.EntrancePasses)
        {
            var combinedFee = new CombinedFee("Pass", entrancePass);
            CombinedFees.Add(combinedFee);
        }

        foreach (var phoneNumber in Park.Contacts.PhoneNumbers)
            PhoneContacts.Add(phoneNumber);

        foreach (var emailContact in Park.Contacts.EmailAddresses)
            EmailContacts.Add(emailContact);
    }
}
