/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class ResultError
{
    public ErrorInfo Error { get; set; }
}

public class ErrorInfo
{
    public string Code { get; set; }
    public string Message { get; set; }
}
