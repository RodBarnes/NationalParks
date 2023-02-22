namespace NationalParks.ViewModels;

public partial class CampsitesVM : CollapsibleViewVM
{
    [ObservableProperty] Campground campground;

    public CampsitesVM(string title, bool isOpen, Campground campground) : base(title, isOpen)
    {
        Campground = campground;
        HasContent = true;
    }
}
