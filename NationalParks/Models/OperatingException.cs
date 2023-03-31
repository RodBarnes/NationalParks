/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class OperatingException
{
    public Hours ExceptionHours { get; set; }
    public DateTime StartDate { get; set; }
    public string Name { get; set; }
    public DateTime EndDate { get; set; }
}
