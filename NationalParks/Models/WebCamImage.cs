namespace NationalParks.Models
{
    public class WebCamImage : Image
    {
        public string Description { get; set; }
        public List<WebCamCrop> Crops { get; set; }
    }
}
