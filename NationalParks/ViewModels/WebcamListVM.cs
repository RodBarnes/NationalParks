using NationalParks.Services;

namespace NationalParks.ViewModels
{
    public partial class WebcamListVM : BaseVM
    {
        readonly DataService dataService;
        readonly IConnectivity connectivity;

        public WebcamListVM(DataService dataService, IConnectivity connectivity)
        {
            Title = "Webcams";
            this.dataService = dataService;
            this.connectivity = connectivity;
        }
    }
}
