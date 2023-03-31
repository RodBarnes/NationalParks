/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

public partial class ProgressBarVM : ObservableObject
{
    [ObservableProperty] double position;
    [ObservableProperty] string text;
    [ObservableProperty] bool isVisible;

    [RelayCommand]
    public void CancelProgress()
    {
        IsVisible = false;
    }

}
