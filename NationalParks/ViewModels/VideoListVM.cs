namespace NationalParks.ViewModels;

public partial class VideoListVM : ListVM
{
    public VideoListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Videos";
        Term = ResultVideos.Term;
        FilterName = "Video";
        AllowFilterStates = true;
    }
}
