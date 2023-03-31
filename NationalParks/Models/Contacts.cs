/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class Contacts
{
    public List<PhoneContact> PhoneNumbers { get; set; }
    public List<EmailContact> EmailAddresses { get; set; }
}
