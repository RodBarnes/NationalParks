namespace NationalParks.ViewModels;

[QueryProperty(nameof(Tour), "Tour")]
public partial class TourImageListVM : BaseVM
{
    // Query properties
    [ObservableProperty] Tour tour;

    public TourImageListVM()
    {
        Title = "Images";
    }

    [RelayCommand]
    async Task GoToImage(Models.Image image)
    {
        await Shell.Current.DisplayAlert($"Image", $"{image.Title}\n{image.Url}", "OK");
    }

}
