﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Webcam), "Model")]
public partial class WebcamDetailVM : DetailVM
{
    [ObservableProperty] Webcam webcam;
    [ObservableProperty] CollapsibleViewVM relatedParksVM;

    public WebcamDetailVM(IMap map) : base(map)
    {
        Title = "Webcam";

        RelatedParksVM = new CollapsibleViewVM("Related Parks", false);
    }
}
