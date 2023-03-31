/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public class Article : BaseModel
{
    public string GeometryPoiId { get; set; }
    public string ListingDescription
    {
        get { return Description; }
        set { Description = value; }
    }
    public Image ListingImage { get; set; }
    public List<RelatedPark> RelatedParks { get; set; }
    public string LatLong { get; set; }

    public new void FillMainImage()
    {
        if (ListingImage != null && !String.IsNullOrEmpty(ListingImage.Url))
        {
            MainImage = ImageSource.FromUri(new Uri(ListingImage.Url));
        }
        else
        {
            MainImage = ImageSource.FromFile("nps");
        }
    }
}
