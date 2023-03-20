namespace NationalParks.ViewModels;

public partial class CollapsibleTextVM : CollapsibleViewVM
{
    [ObservableProperty] string text;
    [ObservableProperty] string url;
    [ObservableProperty] bool hasUrl;

    public CollapsibleTextVM(string title, bool isOpen, string text, string url = "") : base(title, isOpen)
    {
        Text = text;
        Url = url;
        HasContent = !String.IsNullOrEmpty(Text);
        HasUrl = !String.IsNullOrEmpty(Url);
    }
}
