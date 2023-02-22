namespace NationalParks.ViewModels;

public partial class MultimediaVM : CollapsibleViewVM
{
    [ObservableProperty] List<Multimedia> items;

    public MultimediaVM(string title, bool isOpen, List<Multimedia> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
