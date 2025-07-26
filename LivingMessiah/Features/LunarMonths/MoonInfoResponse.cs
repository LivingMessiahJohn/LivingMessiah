public class MoonInfoResponse
{
    public string? Status { get; set; }
    public MoonInfoData? Data { get; set; }
}

public class MoonInfoData
{
    public string? Moonrise { get; set; }
    public string? Moonset { get; set; }
    public string? Azimuth { get; set; }
}