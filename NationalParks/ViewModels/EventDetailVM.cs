﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Event), "Model")]
public partial class EventDetailVM : DetailVM
{
    [ObservableProperty] Event npsEvent;
    [ObservableProperty] AvatarVM avatarVM;

    public EventDetailVM(IMap map) : base(map)
    {
        Title = "Events";
    }

    [RelayCommand]
    public void PopulateData()
    {
        AvatarVM = new AvatarVM(NpsEvent.MainImage);
    }
}
