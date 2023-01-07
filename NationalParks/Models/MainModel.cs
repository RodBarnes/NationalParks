namespace NationalParks.Models
{
    public partial class MainModel
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<Image> Images { get; set; }

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
                            break;
                        }
                    }
                }

                source ??= ImageSource.FromFile("nps.png");

                return source;
            }
        }
        public double DLatitude
        {
            get
            {
                if (double.TryParse(Latitude, out double d))
                {
                    return d;
                }
                else
                {
                    return -1;
                }
            }
        }
        public double DLongitude
        {
            get
            {
                if (double.TryParse(Longitude, out double d))
                {
                    return d;
                }
                else
                {
                    return -1;
                }
            }
        }
        public bool HasUrl => !String.IsNullOrEmpty(Url);
    }
}
