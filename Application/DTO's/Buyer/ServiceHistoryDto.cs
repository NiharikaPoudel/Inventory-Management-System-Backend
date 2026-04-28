namespace Autolaya.Application.DTOs.Buyer;

public class ServiceHistoryDto
{
    public int Id { get; set; }
    public DateTime ServiceDate { get; set; }
    public string VehicleName { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public bool ReviewPending { get; set; }
}