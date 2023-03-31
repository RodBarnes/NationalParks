/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class Alert
{
    public string Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string ParkCode { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string LastIndexedDate { get; set; }
}
