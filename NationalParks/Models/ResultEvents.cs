/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class ResultEvents : Result
{
    public const Terms Term = Terms.events;
    public List<Event> Data { get; set; }
}
