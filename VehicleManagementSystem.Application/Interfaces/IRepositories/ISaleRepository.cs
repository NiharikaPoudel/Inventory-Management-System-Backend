using VehicleManagementSystem.Domain.Models;

namespace VehicleManagementSystem.Application.Interfaces.IRepositories
{
    public interface ISaleRepository
    {
        Task<Sale> AddSaleAsync(Sale sale);

        Task<List<Sale>> GetAllSalesAsync();

        Task<Sale?> GetSaleByIdAsync(int id);
    }
}