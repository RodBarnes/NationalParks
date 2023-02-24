namespace NationalParks.ViewModels;

public partial class ParkingLotsVM : CollapsibleViewVM
{
    readonly IMap map;
    [ObservableProperty] List<ParkingLot> items;

    public ParkingLotsVM(IMap map, string title, bool isOpen, List<ParkingLot> items) : base(title, isOpen)
    {
        this.map = map;
        Items = items;
        HasContent = Items?.Count > 0;
    }

    [RelayCommand]
    async Task GoToParkingLot(ParkingLot lot)
    {
        //await Shell.Current.DisplayAlert("Parking Lot", $"ManagedBy:{lot.ManagedByOrganization}, Description{lot.Description}", "OK");
        if (lot.DLatitude < 0)
        {
            await Shell.Current.DisplayAlert("No location", "Location coordinates are not provided.  Review the description for possible directions or related landmarks.", "OK");
            return;
        }

        try
        {
            await map.OpenAsync(lot.DLatitude, lot.DLongitude, new MapLaunchOptions
            {
                Name = lot.Title,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }
}
