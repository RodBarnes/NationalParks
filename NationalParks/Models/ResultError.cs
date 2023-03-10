namespace NationalParks.Models;

public class ResultError
{
    public ErrorInfo Error { get; set; }
}

public class ErrorInfo
{
    public string Code { get; set; }
    public string Message { get; set; }
}
