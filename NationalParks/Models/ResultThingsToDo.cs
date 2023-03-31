/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class ResultThingsToDo : Result
{
    public const Terms Term = Terms.thingstodo;
    public List<ThingToDo> Data { get; set; }
}
