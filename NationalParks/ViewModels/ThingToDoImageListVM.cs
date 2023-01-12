﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Images), "Images")]
public partial class ThingToDoImageListVM : BaseVM
{
    // Query properties
    [ObservableProperty] List<Models.Image> images;

    public ThingToDoImageListVM()
    {
        Title = "Images";
    }

    [RelayCommand]
    async Task GoToImage(Models.Image image)
    {
        await Shell.Current.DisplayAlert($"Image", $"{image.Title}\n{image.Url}", "OK");
    }
}
