namespace NationalParks.ViewModels
{
    public partial class CollapsibleViewVM : ObservableObject
    {
        [ObservableProperty] bool hasContent;
        [ObservableProperty] string icon;
        [ObservableProperty] string title;
        [ObservableProperty] bool isOpen;

        private readonly string openIcon = "arrow_down_green";
        private readonly string closeIcon= "arrow_up_green";

        public CollapsibleViewVM(string title, bool isOpen)
        {
            Title = title;
            IsOpen = isOpen;
            Icon = (IsOpen) ? closeIcon : openIcon;
        }

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
