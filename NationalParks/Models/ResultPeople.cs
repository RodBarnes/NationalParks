/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public partial class ResultPeople : Result
{
    public const Terms Term = Terms.people;
    public List<Person> Data { get; set; }
}
