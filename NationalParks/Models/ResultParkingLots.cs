namespace NationalParks.Models;

public class ResultParkingLots : Result
{
    public const Terms Term = Terms.parkinglots;
    public List<ParkingLot> Data { get; set; }
}
