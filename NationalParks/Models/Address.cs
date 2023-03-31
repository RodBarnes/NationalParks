/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿using System.Text;

namespace NationalParks.Models;

public class Address
{
    public string Type { get; set; }
    public string StateCode { get; set; }
    public string PostalCode { get; set; }
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string Line3 { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("");

        if (!String.IsNullOrEmpty(Line1)) { sb.Append($"{Line1}"); }
        if (!String.IsNullOrEmpty(Line2)) { sb.Append($", {Line2}"); }
        if (!String.IsNullOrEmpty(Line3)) { sb.Append($", {Line3}"); }
        if (!String.IsNullOrEmpty(StateCode)) { sb.Append($", {StateCode}"); }
        if (!String.IsNullOrEmpty(PostalCode)) { sb.Append($"  {PostalCode}"); }

        return sb.ToString();
    }
}
