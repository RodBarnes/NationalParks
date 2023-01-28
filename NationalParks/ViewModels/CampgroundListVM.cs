namespace NationalParks.ViewModels;

public partial class CampgroundListVM : ListVM
{
    public CampgroundListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Campgrounds";
        Term = ResultCampgrounds.Term;
        FilterName = "Campground";
    }
}
