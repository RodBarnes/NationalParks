/*
 * Copyright (c) 2022 Rod Barnes
 * See the LICENSE.txt file in the project root for specific restrictions.
 */
﻿namespace NationalParks.Models;

public partial class NewsRelease : BaseModel
{
    public string ParkCode { get; set; }
    public string Abstract
    {
        get { return Description; }
        set { Description = value; }
    }
    public Image Image { get; set; }
    public List<RelatedPark> RelatedParks { get; set; }
    public List<Organization> RelatedOrganizations { get; set; }
    public string GeometryPoiId { get; set; }
    public string ReleaseDate { get; set; }
    public string Credit { get; set; }
    public string LastIndexedDate { get; set; }

    #region Derived Properties

    public bool HasCredit { get => !String.IsNullOrEmpty(Credit); }

    #endregion

    public new void FillMainImage()
    {
        if (Image != null && !String.IsNullOrEmpty(Image.Url))
        {
            MainImage = ImageSource.FromUri(new Uri(Image.Url));
        }
        else
        {
            MainImage = ImageSource.FromFile("nps");
        }
    }
}
