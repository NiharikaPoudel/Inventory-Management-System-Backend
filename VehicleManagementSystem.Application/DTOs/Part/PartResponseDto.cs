namespace VehicleManagementSystem.DTOs.Part
{
    public class PartResponseDto
    {
        public int Id { get; set; }

        public string PartName { get; set; } = string.Empty;

        public string PartNumber { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public int? VendorId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}