namespace NationalParks.Models
{
    public partial class MainModel
    {
        // Base properties
        public string Id { get; set; }
        public string Url { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<Image> Images { get; set; }

        // Derived properties
        public ImageSource MainImage => GetMainImage();
        public double DLatitude => double.TryParse(Latitude, out double d) ? d : -1;
        public double DLongitude => double.TryParse(Longitude, out double d) ? d : -1;
        public bool HasUrl => !String.IsNullOrEmpty(Url);

        private ImageSource GetMainImage()
        {
            ImageSource source = null;

            if (Images?.Count > 0)
            {
                foreach (var image in Images)
                {
                    if (!String.IsNullOrEmpty(image.Url))
                    {
                        source = ImageSource.FromUri(new Uri(image.Url));
                        break;
                    }
                }
            }

            source ??= ImageSource.FromFile("nps.png");
            return source;
        }
    }
}
