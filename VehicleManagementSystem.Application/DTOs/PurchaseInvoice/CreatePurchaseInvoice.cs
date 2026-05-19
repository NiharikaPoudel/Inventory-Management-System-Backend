using System.ComponentModel.DataAnnotations;

namespace VehicleManagementSystem.DTOs.PurchaseInvoice
{
    public class CreatePurchaseInvoiceDto
    {
        [Required]
        public int VendorId { get; set; }

        [Required]
        public int PartId { get; set; }

        [Required]
        public string PartName { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int QuantityPurchased { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
        public decimal UnitPrice { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}