namespace NationalParks.ViewModels;

public partial class AlertsVM : CollapsibleViewVM
{
    [ObservableProperty] ICollection<Alert> items;

    public AlertsVM(string title, bool isOpen, ICollection<Alert> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
