/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public partial class ResultActivities : Result
{
    public const Terms Term = Terms.activities;
    public List<Activity> Data { get; set; }
}
