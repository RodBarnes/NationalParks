/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

internal class ResultNewsReleases : Result
{
    public const Terms Term = Terms.newsreleases;
    public List<NewsRelease> Data { get; set; }
}
