﻿namespace NationalParks.Models;

public class Webcam : BaseModel
{
    public ICollection<RelatedPark> RelatedParks { get; set; }
    public string Status { get; set; }
    public string StatusMessage { get; set; }
    public bool IsStreaming { get; set; }
    public ICollection<string> Tags { get; set; }
    public string GeometryPoiId { get; set; }
    public string Credit { get; set; }

    #region Derived Properties

    public bool HasRelatedParks => (RelatedParks is not null) && RelatedParks.Count > 0;

    #endregion
}
