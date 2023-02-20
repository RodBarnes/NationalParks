﻿namespace NationalParks.ViewModels;

public partial class DirectionsVM : CollapsibleViewVM
{
    [ObservableProperty] string physicalAddress;
    [ObservableProperty] string directions;
    [ObservableProperty] bool hasAddress;
    [ObservableProperty] bool hasDirections;

    public DirectionsVM(string title, bool isOpen, string address = "", string directions = "") : base(title, isOpen)
    {
        PhysicalAddress = address;
        Directions = directions;
        HasAddress = !String.IsNullOrEmpty(PhysicalAddress);
        HasDirections = !String.IsNullOrEmpty(Directions);
        HasContent = HasAddress || HasDirections;
    }
}
