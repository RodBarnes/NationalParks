﻿namespace NationalParks.Models
{
    public class ExceptionHours
    {
        public DateOnly StartDate { get; set; }
        public string Name { get; set; }
        public DateOnly EndDate { get; set; }
    }
}