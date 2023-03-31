/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿using System.Text;

namespace NationalParks.Models;

public class Hours
{
    private const string MonAbbrv = "Mon";
    private const string TueAbbrv = "Tue";
    private const string WedAbbrv = "Wed";
    private const string ThuAbbrv = "Thu";
    private const string FriAbbrv = "Fri";
    private const string SatAbbrv = "Sat";
    private const string SunAbbrv = "Sun";

    public string Monday { get; set; }
    public string Tuesday { get; set; }
    public string Wednesday { get; set; }
    public string Thursday { get; set; }
    public string Friday { get; set; }
    public string Saturday { get; set; }
    public string Sunday { get; set; }

    public override string ToString()
    {
        var sbKeys = new StringBuilder();
        var sbDays = new StringBuilder();

        // Key is the value; e.g., "Closed"
        // Value is list of abbreviated property names; e.g., Mon
        var dict = new Dictionary<string, ICollection<string>>();

        CheckForDailyHours(dict, MonAbbrv, Monday);
        CheckForDailyHours(dict, TueAbbrv, Tuesday);
        CheckForDailyHours(dict, WedAbbrv, Wednesday);
        CheckForDailyHours(dict, ThuAbbrv, Thursday);
        CheckForDailyHours(dict, FriAbbrv, Friday);
        CheckForDailyHours(dict, SatAbbrv, Saturday);
        CheckForDailyHours(dict, SunAbbrv, Sunday);

        foreach (var key in dict.Keys)
        {
            foreach (var item in dict[key])
            {
                if (sbDays.Length == 0)
                {
                    sbDays.Append(item);

                }
                else
                {
                    sbDays.Append($",{item}");
                }
            }
            sbKeys.Append($"{sbDays.ToString()}: {key}\n");
            sbDays.Clear();
        }

        return sbKeys.ToString();
    }

    private static void CheckForDailyHours(Dictionary<string, ICollection<string>> dict, string dayName, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            if (dict.ContainsKey(value))
            {
                var dayNames = dict[value];
                switch (dayName)
                {
                    case TueAbbrv:
                        AddDayName(dayName, MonAbbrv, dayNames);
                        break;
                    case WedAbbrv:
                        AddDayName(dayName, TueAbbrv, dayNames);
                        break;
                    case ThuAbbrv:
                        AddDayName(dayName, WedAbbrv, dayNames);
                        break;
                    case FriAbbrv:
                        AddDayName(dayName, ThuAbbrv, dayNames);
                        break;
                    case SatAbbrv:
                        AddDayName(dayName, FriAbbrv, dayNames);
                        break;
                    case SunAbbrv:
                        AddDayName(dayName, SatAbbrv, dayNames);
                        break;
                }
            }
            else
            {
                var dayNames = new List<string> { dayName };
                dict.Add(value, dayNames);
            }
        }
    }

    private static void AddDayName(string dayName, string prevName, ICollection<string> dayNames)
    {
        // Left this as a separate method in case later decision is made to try and use '-'
        // to separate contigous list; i.e., Mon-Thu instead of Mon,Tue,Wed,Thu.
        dayNames.Add(dayName);
    }
}
