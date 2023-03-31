/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class OperatingHours
{
    public List<OperatingException> Exceptions { get; set; }
    public string Description { get; set; }
    public Hours StandardHours { get; set; }
    public string Name { get; set; }
}
