/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

public partial class RelatedParksVM : CollapsibleViewVM
{
    [ObservableProperty] List<RelatedPark> items;

    public RelatedParksVM(string title, bool isOpen, List<RelatedPark> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
