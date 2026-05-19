using VehicleManagementSystem.DTOs.Report;

namespace VehicleManagementSystem.Application.Interfaces.IServices
{
    public interface IReportService
    {
        Task<FinancialReportDto> GetDailyReportAsync(DateTime date);

        Task<FinancialReportDto> GetMonthlyReportAsync(int year, int month);

        Task<FinancialReportDto> GetYearlyReportAsync(int year);
    }
}