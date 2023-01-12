namespace NationalParks.ViewModels
{
    [QueryProperty(nameof(Models.Webcam), "Webcam")]
    public partial class WebcamDetailVM : DetailVM
    {
        [ObservableProperty] Webcam webcam;

        [ObservableProperty] Dictionary<string, object> openMapDict;

        //[ObservableProperty] Dictionary<string, object> goToImagesDict;

        [ObservableProperty] CollapsibleViewVM relatedParksVM;

        public WebcamDetailVM(IMap map) : base(map)
        {
            Title = "Webcam";

            RelatedParksVM = new CollapsibleViewVM("Related Parks", false);
        }

        public void PopulateData()
        {
            OpenMapDict = new Dictionary<string, object>
            {
                { "Latitude", Webcam.Latitude },
                { "Longitude", Webcam.Longitude },
                { "Name", Webcam.Title }
            };

            //GoToImagesDict = new Dictionary<string, object>
            //{
            //    { "PageName", nameof(WebcamImageListPage) },
            //    { "ParamName", "Images" },
            //    { "Object", Webcam.Images }
            //};
        }
    }
}
