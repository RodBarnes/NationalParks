/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public partial class ResultWebcams : Result
{
    public const Terms Term = Terms.webcams;
    public List<Webcam> Data { get; set; }
}
