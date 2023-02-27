namespace NationalParks.ViewModels;

public partial class RelatedMultimediaVM : CollapsibleViewVM
{
    [ObservableProperty] ICollection<RelatedMultimedia> items;

    public RelatedMultimediaVM(string title, bool isOpen, ICollection<RelatedMultimedia> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
