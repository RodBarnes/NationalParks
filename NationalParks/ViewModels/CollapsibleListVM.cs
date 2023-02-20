﻿namespace NationalParks.ViewModels
{
    public partial class CollapsibleListVM : CollapsibleViewVM
    {
        [ObservableProperty] List<object> items;

        public CollapsibleListVM(string title, bool isOpen, List<object> items) : base(title, isOpen)
        {
            Items = items;
            HasContent = Items?.Count > 0;
        }
    }
}
