using VehicleManagementSystem.Application.Interfaces.IRepositories;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.Domain.Models;
using VehicleManagementSystem.DTOs.Part;

namespace VehicleManagementSystem.Infrastructure.Services
{
    public class PartService : IPartService
    {
        private readonly IPartRepository _repository;

        public PartService(IPartRepository repository)
        {
            _repository = repository;
        }

        public async Task<PartResponseDto> CreatePartAsync(CreatePartDto dto)
        {
            var part = new Part
            {
                PartName = dto.PartName,
                PartNumber = dto.PartNumber,
                Category = dto.Category,
                Quantity = dto.Quantity,
                PurchasePrice = dto.PurchasePrice,
                SellingPrice = dto.SellingPrice,
                VendorId = dto.VendorId
            };

            var created = await _repository.AddAsync(part);
            return MapToDto(created);
        }

        public async Task<List<PartResponseDto>> GetAllPartsAsync()
        {
            var parts = await _repository.GetAllAsync();
            return parts.Select(MapToDto).ToList();
        }

        public async Task<PartResponseDto?> GetPartByIdAsync(int id)
        {
            var part = await _repository.GetByIdAsync(id);

            if (part == null)
                return null;

            return MapToDto(part);
        }

        public async Task<PartResponseDto?> UpdatePartAsync(int id, UpdatePartDto dto)
        {
            var part = new Part
            {
                PartName = dto.PartName,
                PartNumber = dto.PartNumber,
                Category = dto.Category,
                Quantity = dto.Quantity,
                PurchasePrice = dto.PurchasePrice,
                SellingPrice = dto.SellingPrice,
                VendorId = dto.VendorId
            };

            var updated = await _repository.UpdateAsync(id, part);

            if (updated == null)
                return null;

            return MapToDto(updated);
        }

        public async Task<bool> DeletePartAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static PartResponseDto MapToDto(Part part)
        {
            return new PartResponseDto
            {
                Id = part.Id,
                PartName = part.PartName,
                PartNumber = part.PartNumber,
                Category = part.Category,
                Quantity = part.Quantity,
                PurchasePrice = part.PurchasePrice,
                SellingPrice = part.SellingPrice,
                VendorId = part.VendorId,
                CreatedAt = part.CreatedAt
            };
        }
    }
}