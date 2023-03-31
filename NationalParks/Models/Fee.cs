/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class Fee
{
    public Fee() { }

    public Fee(Fee fee)
    {
        Cost = fee.Cost;
        Description = fee.Description;
        Title = fee.Title;
    }

    public string Cost { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
}
