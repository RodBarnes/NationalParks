namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Campground), "Campground")]
    public partial class CampgroundImageListVM : BaseVM
    {
        // Query properties
        [ObservableProperty]
        Campground campground;

        public ObservableCollection<Models.Image> Images { get; } = new();

        public CampgroundImageListVM()
        {
            Title = "Images";
        }

        public void PopulateData()
        {
            foreach (var image in Campground.Images)
            {
                //var img = ImageSource.FromUri(new Uri(image.Url));
                Images.Add(image);
            }
        }

        [RelayCommand]
        async Task GoToImage(Models.Image image)
        {
            await Shell.Current.DisplayAlert($"Image", $"{image.Title}\n{image.Url}", "OK");
        }
    }
}
