namespace Autolaya.Domain.Models;

public class Vehicle
{
    public int Id { get; set; }
    public string VehicleCode { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<ServiceHistory> ServiceHistories { get; set; } = new List<ServiceHistory>();
}