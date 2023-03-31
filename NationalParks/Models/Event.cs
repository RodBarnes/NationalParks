/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class Event : BaseModel
{
    public string Location { get; set; }
    public string UpdateUser { get; set; }
    public string ContactName { get; set; }
    public string ContactTelephoneNumber { get; set; }
    public string RecurrenceDateEnd { get; set; }
    public string Datestart { get; set; }
    public string IsRecurring { get; set; }
    public string DateTimeUpdated { get; set; }
    public string PortalName { get; set; }
    public List<string> Types { get; set; }
    public string CreateUser { get; set; }
    public string IsFree { get; set; }
    public string ContactEmailAddress { get; set; }
    public string RegresUrl { get; set; }
    public string Category { get; set; }
    public string ImageIdlist { get; set; }
    public string IsRegresRequired { get; set; }
    public string OrganizationName { get; set; }
    public string IsAllDay { get; set; }
    public string DateEnd { get; set; }
    public string SiteCode { get; set; }
    public string InfoUrl { get; set; }
    public List<Time> Times { get; set; }
    public string RegresInfo { get; set; }
    public string TimeInfo { get; set; }
    public string CategoryId { get; set; }
    public string EventId { get; set; }
    public string ParkFullname { get; set; }
    public string RecurrenceDateStart { get; set; }
    public string Date { get; set; }
    public string SiteType { get; set; }
    public string FeeInfo { get; set; }
    public string RecurrenceRule { get; set; }
    public List<string> Dates { get; set; }
    public string DateTimeCreated { get; set; }
    public string SubjectName { get; set; }
    public List<string> Tags { get; set; }
}
