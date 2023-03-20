using CommunityToolkit.Maui.Alerts;

namespace NationalParks.ViewModels;

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

    [RelayCommand]
    public async Task Copy(object obj)
    {
        // Put the contents in the clipboard
        await Clipboard.Default.SetTextAsync(obj.ToString());
        await Toast.Make("Text copied to clipboard.").Show();
    }
}
