/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class ResultTours : Result
{
    public const Terms Term = Terms.tours;
    public List<Tour> Data { get; set; }
}
