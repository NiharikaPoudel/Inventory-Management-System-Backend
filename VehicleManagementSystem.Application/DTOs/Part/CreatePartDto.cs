using System.ComponentModel.DataAnnotations;
namespace VehicleManagementSystem.DTOs.Part
{
    public class CreatePartDto
    {
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
    }
}