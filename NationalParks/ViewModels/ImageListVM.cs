/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Images), "Images")]
public partial class ImageListVM : BaseVM
{
    // Query properties
    [ObservableProperty] List<Models.Image> images;

    public ImageListVM()
    {
        Title = "Images";
    }

    [RelayCommand]
    async Task GoToImage(Models.Image image)
    {
        await Launcher.OpenAsync(image.Url);
    }
}
