/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

public partial class AmenitiesVM : CollapsibleViewVM
{
    [ObservableProperty] Campground campground;

    public AmenitiesVM(string title, bool isOpen, Campground campground) : base(title, isOpen)
    {
        Campground = campground;
        HasContent = true;
    }
}
