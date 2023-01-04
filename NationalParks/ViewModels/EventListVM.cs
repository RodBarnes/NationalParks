namespace NationalParks.ViewModels
{
    public partial class EventListVM : BaseVM
    {
        public ObservableCollection<Models.Event> Events { get; } = new();

        [ObservableProperty]
        bool isRefreshing;
    }
}
