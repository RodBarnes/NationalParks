namespace NationalParks.Models
{
    public class WebcamImage : Image
    {
        public string Description { get; set; }
        public List<WebcamCrop> Crops { get; set; }
    }
}
