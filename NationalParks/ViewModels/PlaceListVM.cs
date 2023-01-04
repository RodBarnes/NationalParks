namespace NationalParks.ViewModels
{
    public partial class PlaceListVM : BaseVM
    {
        public ObservableCollection<Models.Place> Places { get; } = new();

        [ObservableProperty]
        bool isRefreshing;
    }
}
