namespace NationalParks.ViewModels;

public partial class MessageVM : ObservableObject
{
    [ObservableProperty] string messageText;
    [ObservableProperty] string buttonText;

    [ObservableProperty] bool show;

    public MessageVM()
    {
        Show = false;
        ButtonText = "OK";
        MessageText = "Message";
    }

    public void ShowMessage(string msg = "", string button = "")
    {
        if (!String.IsNullOrEmpty(msg))
        {
            MessageText = msg;
        }
        if (!String.IsNullOrEmpty(button))
        {
            ButtonText = button;
        }
        Show = true;
    }

    [RelayCommand]
    public void HideMessage()
    {
        Show = false;
    }
}
