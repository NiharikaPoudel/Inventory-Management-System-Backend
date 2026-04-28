namespace Autolaya.Domain.Models;

public class Appointment
{
    public int Id { get; set; }
    public string AppointmentCode { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public int VehicleId { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string TimeSlot { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";

    public Customer Customer { get; set; } = null!;
    public Vehicle Vehicle { get; set; } = null!;
}