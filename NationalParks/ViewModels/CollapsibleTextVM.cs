namespace NationalParks.ViewModels
{
    public partial class CollapsibleTextVM : CollapsibleViewVM
    {
        [ObservableProperty] string text;

        public CollapsibleTextVM(string title, bool isOpen) : base(title, isOpen)
        {

        }
    }
}
