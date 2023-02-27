namespace NationalParks.Models;

public class ResultParkingLots : Result
{
    public const string Term = "parkinglots";
    public ICollection<ParkingLot> Data { get; set; }
}
