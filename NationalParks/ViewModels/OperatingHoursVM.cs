namespace NationalParks.ViewModels;

public partial class OperatingHoursVM : CollapsibleViewVM
{
    [ObservableProperty] ICollection<OperatingHours> items;

    public OperatingHoursVM(string title, bool isOpen, ICollection<OperatingHours> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
