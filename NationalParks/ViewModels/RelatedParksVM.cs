namespace NationalParks.ViewModels;

public partial class RelatedParksVM : CollapsibleViewVM
{
    [ObservableProperty] List<RelatedPark> items;

    public RelatedParksVM(string title, bool isOpen) : base(title, isOpen)
    {

    }
}
