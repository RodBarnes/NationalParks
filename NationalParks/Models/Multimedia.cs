/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public partial class Multimedia : BaseModel
{
    public string PermalinkUrl { get; set; }
    public Splashimage SplashImage { get; set; }
    public List<RelatedPark> RelatedParks { get; set; }
    public List<string> Tags { get; set; }
    public string AudioDescription { get; set; }
    public string AudioDescriptionUrl { get; set; }
    public string GeometryPoiId { get; set; }
    public int? DurationMs { get; set; }
    public string Credit { get; set; }
    public string Transcript { get; set; }
    public string CallToAction { get; set; }
    public string CallToActionUrl { get; set; }
    public bool AudioDescribedBuiltIn { get; set; }
    public bool HasOpenCaptions { get; set; }
    public bool IsBRoll { get; set; }
    public List<Captionfile> CaptionFiles { get; set; }
    public List<Specification> Versions { get; set; }

    #region Derived Properties

    public Specification MainVersion
    {
        get
        {
            if (Versions.Count > 0)
            {
                return Versions.OrderByDescending(v => v.HeightPixels).First();
            }
            else
            {
                return Versions.First();
            }
        }
    }
    public string Duration => GetDuration(DurationMs);

    public bool HasCredit => !String.IsNullOrEmpty(Credit);
    #endregion

    public new void FillMainImage()
    {
        if (SplashImage != null && !String.IsNullOrEmpty(SplashImage.Url))
        {
            MainImage = ImageSource.FromUri(new Uri(SplashImage.Url));
        }
        else
        {
            MainImage = ImageSource.FromFile("nps");
        }
    }

    public string Credits => Credit.Replace(',', '\n');

    private static string GetDuration(int? ms)
    {
        if (ms is null || ms < 0)
            return "0";

        if (!int.TryParse(ms.ToString(), out int mss))
        {
            mss = 0;
        }
        double hrd = mss / 1000d / 60d / 60d;
        int hr = (int)hrd;
        double mnd = (hrd - hr) * 60d;
        int mn = (int)mnd;
        double scd = (mnd - mn) * 60d;
        int sc = (int)scd;

        return $"{hr:00}:{mn:00}:{sc:00}";
    }
}

public class Splashimage
{
    public string Url { get; set; }
}

public class Captionfile
{
    public string Language { get; set; }
    public string FileType { get; set; }
    public string Url { get; set; }
}

public class Specification
{
    public float? FileSizeKb { get; set; }
    public string FileType { get; set; }
    public float AspectRatio { get; set; }
    public int HeightPixels { get; set; }
    public string Url { get; set; }
    public int WidthPixels { get; set; }

    #region Derived Properties

    public string Size => $"{HeightPixels}x{WidthPixels}";
    public bool HasSize => HeightPixels > 0 && WidthPixels > 0;
    public bool HasAspect => AspectRatio > 0;

    #endregion
}
