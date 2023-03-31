/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public partial class ResultParks : Result
{
    public const Terms Term = Terms.parks;
    public List<Park> Data { get; set; }
}
