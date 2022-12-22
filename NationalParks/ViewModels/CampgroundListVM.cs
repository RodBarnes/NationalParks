using NationalParks.Services;

namespace NationalParks.ViewModels
{
    public partial class CampgroundListVM : BaseVM
    {
        readonly DataService dataService;
        readonly IConnectivity connectivity;

        public CampgroundListVM(DataService dataService, IConnectivity connectivity)
        {
            Title = "Campgrounds";
            this.dataService = dataService;
            this.connectivity = connectivity;
        }
    }
}
