namespace VehicleManagementSystem.DTOs.Sale
{
    public class SaleResponseDto
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal FinalAmount { get; set; }

        public DateTime SaleDate { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}