﻿namespace NationalParks.ViewModels
{
    [ObservableObject]
    public partial class CollapsibleTextVM
    {
        [ObservableProperty] string text;

        private readonly string openIcon = "arrow_down_green";
        private readonly string closeIcon= "arrow_up_green";

        public CollapsibleTextVM(string title, bool isOpen)
        {
            Title = title;
            IsOpen = isOpen;
            Icon = (IsOpen) ? closeIcon : openIcon;
        }

        [ObservableProperty] string icon;

        [ObservableProperty] string title;

        [ObservableProperty] bool isOpen;

        [RelayCommand]
        public void Toggle()
        {
            if (IsOpen)
            {
                IsOpen = false;
                Icon = openIcon;
            }
            else
            {
                IsOpen = true;
                Icon = closeIcon;
            }
        }
    }
}
