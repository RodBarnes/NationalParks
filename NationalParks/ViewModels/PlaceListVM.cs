using NationalParks.Services;

namespace NationalParks.ViewModels;

public partial class PlaceListVM : ListVM
{
    public PlaceListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Places";
        Term = ResultPlaces.Term;
        FilterName = "Place";
    }
}
