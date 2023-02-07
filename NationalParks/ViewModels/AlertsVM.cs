namespace NationalParks.ViewModels;

public partial class AlertsVM : CollapsibleViewVM
{
    [ObservableProperty] List<Alert> alerts;

    public AlertsVM(string title, bool isOpen) : base(title, isOpen)
    {

    }
}
