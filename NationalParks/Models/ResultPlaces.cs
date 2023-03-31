/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class ResultPlaces : Result
{
    public const Terms Term = Terms.places;
    public List<Place> Data { get; set; }
}
