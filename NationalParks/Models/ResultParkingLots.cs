/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class ResultParkingLots : Result
{
    public const Terms Term = Terms.parkinglots;
    public List<ParkingLot> Data { get; set; }
}
