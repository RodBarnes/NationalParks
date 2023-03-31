/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.ViewModels;

//[QueryProperty(nameof(Images), "Images")]
public partial class TestVM : BaseVM
{
    // Query properties
    //[ObservableProperty] List<Models.Image> images;

    public TestVM()
    {
        Title = "Test";
    }
}
