namespace VehicleManagementSystem.DTOs.PurchaseInvoice
{
    public class PurchaseInvoiceResponseDto
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; } = string.Empty;
        public string PartName { get; set; } = string.Empty;
        public int QuantityPurchased { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime PurchasedAt { get; set; }
    }
}