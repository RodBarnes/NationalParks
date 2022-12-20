﻿namespace NationalParks.Models
{
    public class WebCam
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<WebCamImage> Images { get; set; }
        public List<WebCamPark> RelatedParks { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public bool IsStreaming { get; set; }
        public List<string> Tags { get; set; }
        public long? Latitude { get; set; }
        public long? Longitude { get; set; }
        public string GeometryPoiId { get; set; }
        public string Credit { get; set; }
    }
}
