/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public partial class ResultVideos : Result
{
    public const Terms Term = Terms.videos;
    public List<Multimedia> Data { get; set; }
}
