namespace Autolaya.Application.DTOs.Buyer;

public class AppointmentDto
{
    public int Id { get; set; }
    public string AppointmentCode { get; set; } = string.Empty;
    public string VehicleName { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string TimeSlot { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

public class CreateAppointmentDto
{
    public int VehicleId { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string TimeSlot { get; set; } = string.Empty;
}