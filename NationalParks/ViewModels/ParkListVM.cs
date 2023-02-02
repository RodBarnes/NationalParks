namespace NationalParks.ViewModels;

public partial class ParkListVM : ListVM
{
    public ParkListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Parks";
        Term = ResultParks.Term;
        FilterName = "Park";
        AllowFilterActivities = true;
        AllowFilterTopics = true;
        AllowFilterStates = true;
    }
}
