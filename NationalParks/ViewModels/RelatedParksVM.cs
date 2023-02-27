namespace NationalParks.ViewModels;

public partial class RelatedParksVM : CollapsibleViewVM
{
    [ObservableProperty] ICollection<RelatedPark> items;

    public RelatedParksVM(string title, bool isOpen, ICollection<RelatedPark> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
