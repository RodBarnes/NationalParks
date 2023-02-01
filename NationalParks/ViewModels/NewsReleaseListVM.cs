namespace NationalParks.ViewModels;

public partial class NewsReleaseListVM : ListVM
{
    public NewsReleaseListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "News Releases";
        Term = ResultNewsReleases.Term;
        FilterName = "NewsRelease";
    }

}
