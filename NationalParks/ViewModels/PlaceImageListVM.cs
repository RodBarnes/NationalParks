namespace NationalParks.ViewModels;

[QueryProperty(nameof(Place), "Place")]
public partial class PlaceImageListVM : BaseVM
{
    // Query properties
    [ObservableProperty] Place place;

    public PlaceImageListVM()
    {
        Title = "Images";
    }

    [RelayCommand]
    async Task GoToImage(Models.Image image)
    {
        await Shell.Current.DisplayAlert($"Image", $"{image.Title}\n{image.Url}", "OK");
    }
}
