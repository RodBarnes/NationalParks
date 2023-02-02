namespace NationalParks.ViewModels;

public partial class WebcamListVM : ListVM
{
    public WebcamListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Webcams";
        Term = ResultWebcams.Term;
        FilterName = "Webcam";
        AllowFilterStates = true;
    }
}
