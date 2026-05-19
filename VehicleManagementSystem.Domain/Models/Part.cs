using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.Domain.Models
{
    public class Part
    {
        public int Id { get; set; }

        [Required]
        public string PartName { get; set; } = string.Empty;

        [Required]
        public string PartNumber { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PurchasePrice { get; set; }

        [Required]
        public decimal SellingPrice { get; set; }

        public int? VendorId { get; set; }

        public Vendor? Vendor { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}