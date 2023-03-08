namespace NationalParks.ViewModels;

public partial class FeesVM : CollapsibleViewVM
{
    [ObservableProperty] List<Fee> items;

    public FeesVM(string title, bool isOpen, List<Fee> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
