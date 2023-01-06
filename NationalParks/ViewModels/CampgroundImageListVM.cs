namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Campground), "Campground")]
    public partial class CampgroundImageListVM : BaseVM
    {
        // Query properties
        [ObservableProperty]
        Campground campground;

        public CampgroundImageListVM()
        {
            Title = "Images";
        }

        [RelayCommand]
        async Task GoToImage(Models.Image image)
        {
            await Shell.Current.DisplayAlert($"Image", $"{image.Title}\n{image.Url}", "OK");
        }
    }
}
