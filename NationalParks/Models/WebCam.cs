namespace NationalParks.Models
{
    public class Webcam
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<WebcamImage> Images { get; set; }
        public List<WebcamPark> RelatedParks { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public bool IsStreaming { get; set; }
        public List<string> Tags { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string GeometryPoiId { get; set; }
        public string Credit { get; set; }
    }
}
