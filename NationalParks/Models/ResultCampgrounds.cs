/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public partial class ResultCampgrounds : Result
{
    public const Terms Term = Terms.campgrounds;
    public List<Campground> Data { get; set; }
}
