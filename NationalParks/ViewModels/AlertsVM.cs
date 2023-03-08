namespace NationalParks.ViewModels;

public partial class AlertsVM : CollapsibleViewVM
{
    [ObservableProperty] List<Alert> items;

    public AlertsVM(string title, bool isOpen, List<Alert> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
