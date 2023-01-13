namespace NationalParks.Models
{
    public partial class BaseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public object Latitude { get; set; }
        public object Longitude { get; set; }
        public List<Image> Images { get; set; }

        // Derived properties
        public ImageSource MainImage => GetMainImage();
        public double DLatitude => double.TryParse(Latitude.ToString(), out double d) ? d : -1;
        public double DLongitude => double.TryParse(Longitude.ToString(), out double d) ? d : -1;
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

            source ??= ImageSource.FromFile("nps");
            return source;
        }
    }
}

