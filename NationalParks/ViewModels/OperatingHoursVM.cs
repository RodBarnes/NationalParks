namespace NationalParks.ViewModels;

public partial class OperatingHoursVM : CollapsibleViewVM
{
    [ObservableProperty] List<OperatingHours> operatingHours;

    public OperatingHoursVM(string title, bool isOpen) : base(title, isOpen)
    {

    }
}
