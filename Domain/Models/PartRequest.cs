namespace Autolaya.Domain.Models;

public class PartRequest
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int VehicleId { get; set; }
    public string PartDescription { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

    public Customer Customer { get; set; } = null!;
    public Vehicle Vehicle { get; set; } = null!;
}