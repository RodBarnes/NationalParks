﻿namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Park), "Park")]
    public partial class ImagesVM: BaseVM
    {
        // Query properties
        [ObservableProperty]
        Park park;

        public ObservableCollection<Models.Image> Images { get; } = new();

        public ImagesVM()
        {
            Title = "Images";
        }

        public void PopulateData()
        {
            foreach (var image in Park.Images)
            {
                //var img = ImageSource.FromUri(new Uri(image.Url));
                Images.Add(image);
            }
        }

        [RelayCommand]
        async Task GoToImage()
        {
            await Shell.Current.DisplayAlert("Image", $"Image to display", "OK");
        }
    }
}
