namespace NationalParks.Models;


public class ParkingLot : BaseModel
{
    public string AltName { get; set; }
    public List<RelatedPark> RelatedParks { get; set; }
    public string GeometryPoiId { get; set; }
    public string ManagedByOrganization { get; set; }
    public string TimeZone { get; set; }
    public string WebcamUrl { get; set; }
    public Contacts Contacts { get; set; }
    public List<Fee> Fees { get; set; }
    public List<OperatingHours> OperatingHours { get; set; }
    public ParkingAccessibility Accessibility { get; set; }
    public ParkingLivestatus LiveStatus { get; set; }

    #region Derived Properties

    public bool HasWebcamUrl => !String.IsNullOrEmpty(WebcamUrl);

    #endregion
}

public class ParkingAccessibility
{
    public bool IsLotAccessibleToDisabled { get; set; }
    public int TotalSpaces { get; set; }
    public int NumberofAdaSpaces { get; set; }
    public int NumberofAdaVanAccessbileSpaces { get; set; }
    public int NumberofAdaStepFreeSpaces { get; set; }
    public int NumberOfOversizeVehicleSpaces { get; set; }
    public string AdaFacilitiesDescription { get; set; }
}

public class ParkingLivestatus
{
    public bool IsActive { get; set; }
    public string Occupancy { get; set; }
    public object EstimatedWaitTimeInMinutes { get; set; }
    public string Description { get; set; }
    public string ExpirationDate { get; set; }
}
