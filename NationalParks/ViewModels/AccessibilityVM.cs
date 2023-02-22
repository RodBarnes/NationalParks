namespace NationalParks.ViewModels;

public partial class AccessibilityVM : CollapsibleViewVM
{
    [ObservableProperty] Campground campground;

    public AccessibilityVM(string title, bool isOpen, Campground campground) : base(title, isOpen)
    {
        Campground = campground;
        HasContent = true;
    }
}
