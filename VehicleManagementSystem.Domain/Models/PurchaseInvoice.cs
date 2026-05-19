using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.Domain.Models
{
    public class PurchaseInvoice
    {
        public int Id { get; set; }

        [Required]
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;

        [Required]
        public string PartName { get; set; } = string.Empty;

        [Required]
        public int QuantityPurchased { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public decimal TotalAmount => QuantityPurchased * UnitPrice;

        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;

        public string Notes { get; set; } = string.Empty;
    }
}