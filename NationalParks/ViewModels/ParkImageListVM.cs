namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Park), "Park")]
    public partial class ParkImageListVM: BaseVM
    {
        // Query properties
        [ObservableProperty] Park park;

        public ParkImageListVM()
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
