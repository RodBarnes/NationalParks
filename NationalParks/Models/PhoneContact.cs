/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class PhoneContact
{
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public string Extension { get; set; }
    public string Type { get; set; }

    #region Derived Properties

    public bool HasExtension { get => !String.IsNullOrEmpty(Extension); }

    #endregion
}
