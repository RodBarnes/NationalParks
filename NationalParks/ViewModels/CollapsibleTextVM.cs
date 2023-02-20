namespace NationalParks.ViewModels
{
    public partial class CollapsibleTextVM : CollapsibleViewVM
    {
        [ObservableProperty] string text;
        [ObservableProperty] string url;
        [ObservableProperty] bool hasUrl;

        public CollapsibleTextVM(string title, bool isOpen) : base(title, isOpen)
        {

        }
    }
}
