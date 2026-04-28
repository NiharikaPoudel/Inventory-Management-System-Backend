namespace Autolaya.Application.DTOs.Buyer;

public class ReviewDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string VehicleName { get; set; } = string.Empty;
    public string ServiceType { get; set; } = string.Empty;
    public decimal StarRating { get; set; }
    public string Comment { get; set; } = string.Empty;
}

public class CreateReviewDto
{
    public int ServiceHistoryId { get; set; }
    public decimal StarRating { get; set; }
    public string Comment { get; set; } = string.Empty;
}

public class PartRequestDto
{
    public int Id { get; set; }
    public string VehicleName { get; set; } = string.Empty;
    public string PartDescription { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime RequestedAt { get; set; }
}

public class CreatePartRequestDto
{
    public int VehicleId { get; set; }
    public string PartDescription { get; set; } = string.Empty;
}