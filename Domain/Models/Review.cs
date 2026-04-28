namespace Autolaya.Domain.Models;

public class Review
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int ServiceHistoryId { get; set; }
    public decimal StarRating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsFeatured { get; set; } = false;

    public Customer Customer { get; set; } = null!;
    public ServiceHistory ServiceHistory { get; set; } = null!;
}