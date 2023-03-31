/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

public partial class AlertsVM : CollapsibleViewVM
{
    [ObservableProperty] List<Alert> items;

    public AlertsVM(string title, bool isOpen, List<Alert> items) : base(title, isOpen)
    {
        Items = items;
        HasContent = Items?.Count > 0;
    }
}
