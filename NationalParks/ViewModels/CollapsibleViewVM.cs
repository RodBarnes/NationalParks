namespace NationalParks.ViewModels
{
    [ObservableObject]
    public partial class CollapsibleViewVM
    {
        private readonly string openIcon = "arrow_down_green";
        private readonly string closeIcon= "arrow_up_green";

        public CollapsibleViewVM(string title, bool isOpen)
        {
            Title = title;
            IsOpen = isOpen;
            if (IsOpen)
            {
                Icon = closeIcon;
            }
            else
            {
                Icon = openIcon;
            }
        }

        [ObservableProperty]
        string icon;

        [ObservableProperty]
        string title;

        [ObservableProperty]
        bool isOpen;

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
