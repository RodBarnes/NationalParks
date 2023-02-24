namespace NationalParks.ViewModels;

public partial class ParkingLotsVM : CollapsibleViewVM
{
    [ObservableProperty] List<ParkingLot> items;

    public ParkingLotsVM(string title, bool isOpen, List<ParkingLot> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
