namespace NationalParks.ViewModels
{
    public partial class CollapsibleListVM : CollapsibleViewVM
    {
        [ObservableProperty] List<object> items;

        public CollapsibleListVM(string title, bool isOpen) : base(title, isOpen)
        {

        }
    }
}
