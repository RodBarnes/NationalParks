namespace NationalParks.Models;

public partial class BaseModel
{
    public string Id { get; set; }
    private string title;
    public string Title
    {
        get => !String.IsNullOrEmpty(title) ? title : Name;
        set => title = value;
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public object Latitude { get; set; }
    public object Longitude { get; set; }
    public List<Image> Images { get; set; }

    #region Derived Properties

    public ImageSource MainImage { get; protected set; }
    public double DLatitude => double.TryParse(Latitude?.ToString(), out double d) ? d : -1;
    public double DLongitude
    {
        get
        {
            var x = double.TryParse(Longitude?.ToString(), out double d) ? d : -1;
            // This is ugly but necessary because some of the longitude data appears with positive
            // values putting them in Asia somewhere!  Since these are all, by definition, National
            // Parks in the U.S.A, force them to negative values if they are positive.
            if (x > 0)
                x = -x;
            return x;
        }
    }
    public bool HasUrl => !String.IsNullOrEmpty(Url);

    #endregion

    public void FillMainImage()
    {
        var image = Images.FirstOrDefault();
        if (image != null && !String.IsNullOrEmpty(image.Url))
        {
            MainImage = ImageSource.FromUri(new Uri(image.Url));
        }
        else
        {
            MainImage = ImageSource.FromFile("nps");
        }
    }
}

