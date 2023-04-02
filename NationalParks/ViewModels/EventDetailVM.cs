/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

[QueryProperty(nameof(Models.Event), "Model")]
public partial class EventDetailVM : DetailVM
{
    [ObservableProperty] Event npsEvent;

    public EventDetailVM(IConnectivity connectivity, IMap map) : base(connectivity, map)
    {
        Title = "Events";
    }

    [RelayCommand]
    public void PopulateData()
    {
        Model = NpsEvent;
    }
}
