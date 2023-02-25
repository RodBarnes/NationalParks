namespace NationalParks.ViewModels;

public partial class AudioListVM : ListVM
{
    public AudioListVM(IConnectivity connectivity, IGeolocation geolocation) : base(connectivity, geolocation)
    {
        BaseTitle = "Audios";
        Term = ResultAudios.Term;
        FilterName = "Audio";
        AllowFilterStates = true;
    }
}
