/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Webcam), "Model")]
public partial class WebcamDetailVM : DetailVM
{
    [ObservableProperty] Webcam webcam;
    [ObservableProperty] RelatedParksVM relatedParks;

    public WebcamDetailVM(IConnectivity connectivity, IMap map) : base(connectivity, map)
    {
        Title = "Webcam";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = Webcam;

        RelatedParks = new RelatedParksVM("Related Parks", false, Webcam.RelatedParks);
    }

    [RelayCommand]
    public async Task GoToWebcam()
    {
        await Launcher.OpenAsync(Webcam.Url);
    }
}
