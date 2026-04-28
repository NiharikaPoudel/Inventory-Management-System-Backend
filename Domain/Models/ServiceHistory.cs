namespace Autolaya.Domain.Models;

public class ServiceHistory
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int CustomerId { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public DateTime ServiceDate { get; set; }
    public decimal TotalCost { get; set; }
    public string? Notes { get; set; }
    public bool ReviewPending { get; set; } = true;

    public Vehicle Vehicle { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public Review? Review { get; set; }
}