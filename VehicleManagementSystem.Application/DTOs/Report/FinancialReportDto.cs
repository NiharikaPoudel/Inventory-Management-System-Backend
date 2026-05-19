namespace VehicleManagementSystem.DTOs.Report
{
    public class FinancialReportDto
    {
        public string ReportType { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TotalSalesCount { get; set; }

        public decimal TotalSalesAmount { get; set; }

        public decimal TotalDiscountAmount { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}