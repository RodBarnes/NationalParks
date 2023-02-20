namespace NationalParks.ViewModels;

public partial class OperatingHoursVM : CollapsibleViewVM
{
    [ObservableProperty] List<OperatingHours> items;

    public OperatingHoursVM(string title, bool isOpen, List<OperatingHours> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
