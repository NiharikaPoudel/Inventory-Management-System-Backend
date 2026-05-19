namespace VehicleManagementSystem.DTOs.Sale
{
    public class SaleResponseDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal FinalAmount { get; set; }

        public DateTime SaleDate { get; set; }

        public string Status { get; set; } = "paid";

        public string Message { get; set; } = string.Empty;
    }
}