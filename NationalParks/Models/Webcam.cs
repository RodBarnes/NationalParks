namespace NationalParks.Models
{
    public class Webcam
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Image> Images { get; set; }
        public List<RelatedPark> RelatedParks { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public bool IsStreaming { get; set; }
        public List<string> Tags { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string GeometryPoiId { get; set; }
        public string Credit { get; set; }

        // Derived properties
        public ImageSource MainImage
        {
            get
            {
                ImageSource source = null;

                if (Images.Count > 0)
                {
                    foreach (var image in Images)
                    {
                        if (!String.IsNullOrEmpty(image.Url))
                        {
                            source = ImageSource.FromUri(new Uri(image.Url));
                        }
                    }
                }

                source ??= ImageSource.FromFile("nps.png");

                return source;
            }
        }
    }
}
