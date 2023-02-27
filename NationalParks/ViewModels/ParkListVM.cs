namespace NationalParks.ViewModels;

public partial class ParkListVM : ListVM
{
    public ParkListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Parks";
        Term = ResultParks.Term;
        FilterName = "Park";
        AllowFilterStates = true;
        // Commenting these since it appears the data cannot be filtered this way
        //AllowFilterActivities = true;
        //AllowFilterTopics = true;
    }
}
