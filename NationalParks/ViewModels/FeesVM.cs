namespace NationalParks.ViewModels;

public partial class FeesVM : CollapsibleViewVM
{
    [ObservableProperty] ICollection<Fee> items;

    public FeesVM(string title, bool isOpen, ICollection<Fee> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
