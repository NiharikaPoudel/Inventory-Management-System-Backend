using VehicleManagementSystem.Domain.Models;

namespace VehicleManagementSystem.Application.Interfaces.IRepositories
{
    // ─── Feature 14 ──────────────────────────────────────────────────────────

    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetSalesByCustomerAsync(int customerId);
        Task<Sale?> GetSaleByIdAsync(int saleId);

        Task<Sale> AddSaleAsync(Sale sale);
        Task<IEnumerable<Sale>> GetAllSalesAsync();
    }
}


//using VehicleManagementSystem.Domain.Models;

//namespace VehicleManagementSystem.Application.Interfaces.IRepositories
//{
//    public interface ISaleRepository
//    {
//        Task<Sale> AddSaleAsync(Sale sale);

//        Task<List<Sale>> GetAllSalesAsync();

//        Task<Sale?> GetSaleByIdAsync(int id);
//    }
//}