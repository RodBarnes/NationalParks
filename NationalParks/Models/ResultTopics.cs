/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public partial class ResultTopics : Result
{
    public const Terms Term = Terms.topics;
    public List<Topic> Data { get; set; }
}
