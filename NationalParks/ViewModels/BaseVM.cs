/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

public partial class BaseVM : ObservableObject
{
    [ObservableProperty] bool isBusy;
    [ObservableProperty] string title;

    public bool IsNotBusy => !IsBusy;
}
