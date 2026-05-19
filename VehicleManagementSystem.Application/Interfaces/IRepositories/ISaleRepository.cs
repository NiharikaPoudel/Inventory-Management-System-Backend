using VehicleManagementSystem.Domain.Models;
using VehicleManagementSystem.DTOs.Sale;

namespace VehicleManagementSystem.Application.Interfaces.IRepositories
{
    public interface ISaleRepository
    {
        Task<Sale> AddSaleAsync(Sale sale);

        Task<List<Sale>> GetAllSalesAsync();

        Task<Sale?> GetSaleByIdAsync(int id);

        Task<List<CustomerReportDto>> GetRegularCustomersAsync();

        Task<List<CustomerReportDto>> GetHighSpendersAsync();

        Task<List<CustomerReportDto>> GetPendingCreditCustomersAsync();
    }
}