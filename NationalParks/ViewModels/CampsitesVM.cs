/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

public partial class CampsitesVM : CollapsibleViewVM
{
    [ObservableProperty] Campground campground;

    public CampsitesVM(string title, bool isOpen, Campground campground) : base(title, isOpen)
    {
        Campground = campground;
        HasContent = true;
    }
}
