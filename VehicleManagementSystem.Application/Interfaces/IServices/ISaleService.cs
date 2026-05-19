using VehicleManagementSystem.DTOs.Sale;

namespace VehicleManagementSystem.Application.Interfaces.IServices
{
    public interface ISaleService
    {
        Task<SaleResponseDto> CreateSaleAsync(CreateSaleDto dto);

        Task<List<SaleResponseDto>> GetAllSalesAsync();

        Task<SaleResponseDto?> GetSaleByIdAsync(int id);

        Task<List<CustomerReportDto>> GetRegularCustomersAsync();

        Task<List<CustomerReportDto>> GetHighSpendersAsync();

        Task<List<CustomerReportDto>> GetPendingCreditCustomersAsync();
    }
}