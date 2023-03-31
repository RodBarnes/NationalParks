/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

public partial class RelatedMultimediaVM : CollapsibleViewVM
{
    [ObservableProperty] List<RelatedMultimedia> items;

    public RelatedMultimediaVM(string title, bool isOpen, List<RelatedMultimedia> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
