namespace NationalParks.ViewModels;

public partial class MessageVM : ObservableObject
{
    [ObservableProperty] string messageText;
    [ObservableProperty] string buttonText;

    [ObservableProperty] bool isVisible;

    public MessageVM()
    {
        IsVisible = false;
        ButtonText = "OK";
        MessageText = "Message";
    }

    public void Show(string msg = "", string button = "")
    {
        if (!String.IsNullOrEmpty(msg))
        {
            MessageText = msg;
        }
        if (!String.IsNullOrEmpty(button))
        {
            ButtonText = button;
        }
        IsVisible = true;
    }

    [RelayCommand]
    public void HideMessage()
    {
        IsVisible = false;
    }
}
