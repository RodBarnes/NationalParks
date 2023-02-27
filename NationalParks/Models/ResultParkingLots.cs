namespace NationalParks.Models;

public class ResultParkingLots : Result
{
    public const string Term = "parkinglots";
    public List<ParkingLot> Data { get; set; }
}
