namespace Autolaya.Application.DTOs.Buyer;

public class VehicleDto
{
    public int Id { get; set; }
    public string VehicleCode { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}