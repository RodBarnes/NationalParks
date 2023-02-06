namespace NationalParks.ViewModels;

public partial class DirectionsVM : CollapsibleViewVM
{
    [ObservableProperty] string physicalAddress;
    [ObservableProperty] string directions;

    public DirectionsVM(string title, bool isOpen) : base(title, isOpen)
    {

    }
}
