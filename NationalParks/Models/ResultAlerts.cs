/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class ResultAlerts : Result
{
    public const Terms Term = Terms.alerts;
    public List<Alert> Data { get; set; }
}
