/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public partial class ResultAudio : Result
{
    public const Terms Term = Terms.audio;
    public List<Multimedia> Data { get; set; }
}
