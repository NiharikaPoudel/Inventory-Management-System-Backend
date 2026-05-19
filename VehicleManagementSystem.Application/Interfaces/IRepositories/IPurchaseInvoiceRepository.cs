using VehicleManagementSystem.Domain.Models;

namespace VehicleManagementSystem.Application.Interfaces.IRepositories
{
    public interface IPurchaseInvoiceRepository
    {
        Task<PurchaseInvoice> CreateAsync(PurchaseInvoice invoice);
        Task<IEnumerable<PurchaseInvoice>> GetAllAsync();
        Task<PurchaseInvoice?> GetByIdAsync(int id);
    }
}