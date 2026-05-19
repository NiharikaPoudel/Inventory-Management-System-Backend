using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.DTOs.Report;
using VehicleManagementSystem.Infrastructure.Persistence;

namespace VehicleManagementSystem.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FinancialReportDto> GetDailyReportAsync(DateTime date)
        {
            var startDate = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
            var endDate = startDate.AddDays(1);

            return await GenerateReportAsync("Daily", startDate, endDate);
        }

        public async Task<FinancialReportDto> GetMonthlyReportAsync(int year, int month)
        {
            var startDate = DateTime.SpecifyKind(new DateTime(year, month, 1), DateTimeKind.Utc);
            var endDate = startDate.AddMonths(1);

            return await GenerateReportAsync("Monthly", startDate, endDate);
        }

        public async Task<FinancialReportDto> GetYearlyReportAsync(int year)
        {
            var startDate = DateTime.SpecifyKind(new DateTime(year, 1, 1), DateTimeKind.Utc);
            var endDate = startDate.AddYears(1);

            return await GenerateReportAsync("Yearly", startDate, endDate);
        }

        private async Task<FinancialReportDto> GenerateReportAsync(
            string reportType,
            DateTime startDate,
            DateTime endDate)
        {
            var sales = await _context.Sales
                .Where(s => s.SaleDate >= startDate && s.SaleDate < endDate)
                .ToListAsync();

            return new FinancialReportDto
            {
                ReportType = reportType,
                StartDate = startDate,
                EndDate = endDate.AddSeconds(-1),
                TotalSalesCount = sales.Count,
                TotalSalesAmount = sales.Sum(s => s.TotalAmount),
                TotalDiscountAmount = sales.Sum(s => s.DiscountAmount),
                TotalRevenue = sales.Sum(s => s.FinalAmount)
            };
        }
    }
}