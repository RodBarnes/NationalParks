namespace NationalParks.ViewModels;

public partial class MultimediaVersionsVM : CollapsibleViewVM
{
    [ObservableProperty] ICollection<Specification> items;

    public MultimediaVersionsVM(string title, bool isOpen, ICollection<Specification> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }

    [RelayCommand]
    async Task GoToMultimedia(string url)
    {
        await Launcher.OpenAsync(url);

    }
}
