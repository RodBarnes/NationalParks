/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class Image
{
    public string Url { get; set; }
    public string Credit { get; set; }
    public string Title { get; set; }
    public string AltText { get; set; }
    public string Description { get; set; }
    public string Caption { get; set; }
    public List<MediaCrop> Crops { get; set; }
}
