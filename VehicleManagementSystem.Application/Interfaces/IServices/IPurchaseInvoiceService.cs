using VehicleManagementSystem.DTOs.PurchaseInvoice;

namespace VehicleManagementSystem.Application.Interfaces.IServices
{
    public interface IPurchaseInvoiceService
    {
        Task<PurchaseInvoiceResponseDto> CreateAsync(CreatePurchaseInvoiceDto dto);
        Task<IEnumerable<PurchaseInvoiceResponseDto>> GetAllAsync();
        Task<PurchaseInvoiceResponseDto?> GetByIdAsync(int id);
    }
}