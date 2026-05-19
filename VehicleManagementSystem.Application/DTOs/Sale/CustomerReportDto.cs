namespace VehicleManagementSystem.DTOs.Sale
{
    public class CustomerReportDto
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TotalVisits { get; set; }
        public decimal TotalSpent { get; set; }
        public decimal PendingCreditAmount { get; set; }
    }
}