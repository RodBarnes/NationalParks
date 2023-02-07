namespace NationalParks.ViewModels;

public partial class FeesVM : CollapsibleViewVM
{
    [ObservableProperty] List<Fee> fees;

    public FeesVM(string title, bool isOpen) : base(title, isOpen)
    {

    }
}
