/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;


public class Tour : BaseModel
{
    public RelatedPark Park { get; set; }
    public List<string> Tags { get; set; }
    public List<Activity> Activities { get; set; }
    public List<Topic> Topics { get; set; }
    public int DurationMin { get; set; }
    public int DurationMax { get; set; }
    public string DurationUnit { get; set; }
    public List<Stop> Stops { get; set; }

    #region Derived Properties

    public string Duration => $"{DurationMin}-{DurationMax}{DurationUnit}";
    public bool HasTags => (Tags is not null) && Tags.Count > 0;
    public bool HasStops => (Stops is not null) && Stops.Count > 0;
    public bool HasTopics => (Topics is not null) && Topics.Count > 0;
    public bool HasActivities => (Activities is not null) && Activities.Count > 0;

    #endregion
}
