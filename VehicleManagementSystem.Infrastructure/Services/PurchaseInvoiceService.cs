using VehicleManagementSystem.Application.Interfaces.IRepositories;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.Domain.Models;
using VehicleManagementSystem.DTOs.PurchaseInvoice;
using VehicleManagementSystem.Infrastructure.Repositories;

namespace VehicleManagementSystem.Infrastructure.Services
{
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private readonly IPurchaseInvoiceRepository _repository;
        private readonly IPartRepository _partRepository;

        public PurchaseInvoiceService(
            IPurchaseInvoiceRepository repository,
            IPartRepository partRepository)
        {
            _repository = repository;
            _partRepository = partRepository;
        }

        public async Task<PurchaseInvoiceResponseDto> CreateAsync(CreatePurchaseInvoiceDto dto)
        {

            var part = await _partRepository.GetByNameAsync(dto.PartName);

            if (part == null)
                throw new Exception("Part not found!");

            if (dto.QuantityPurchased > part.Quantity)
                throw new Exception($"Insufficient stock! Available: {part.Quantity}, Requested: {dto.QuantityPurchased}");

            var invoice = new PurchaseInvoice
            {
                VendorId = dto.VendorId,
                PartName = dto.PartName,
                QuantityPurchased = dto.QuantityPurchased,
                UnitPrice = dto.UnitPrice,
                Notes = dto.Notes
            };

            var created = await _repository.CreateAsync(invoice);
            var withVendor = await _repository.GetByIdAsync(created.Id);

            return MapToDto(withVendor!);
        }

        public async Task<IEnumerable<PurchaseInvoiceResponseDto>> GetAllAsync()
        {
            var invoices = await _repository.GetAllAsync();
            return invoices.Select(MapToDto);
        }

        public async Task<PurchaseInvoiceResponseDto?> GetByIdAsync(int id)
        {
            var invoice = await _repository.GetByIdAsync(id);
            if (invoice == null) return null;
            return MapToDto(invoice);
        }

        private static PurchaseInvoiceResponseDto MapToDto(PurchaseInvoice i) => new()
        {
            Id = i.Id,
            VendorId = i.VendorId,
            VendorName = i.Vendor?.Name ?? "",
            PartName = i.PartName,
            QuantityPurchased = i.QuantityPurchased,
            UnitPrice = i.UnitPrice,
            TotalAmount = i.TotalAmount,
            Notes = i.Notes,
            PurchasedAt = i.PurchasedAt
        };
    }
}