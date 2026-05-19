using VehicleManagementSystem.DTOs.Part;

namespace VehicleManagementSystem.Application.Interfaces.IServices
{
    public interface IPartService
    {
        Task<PartResponseDto> CreatePartAsync(CreatePartDto dto);

        Task<List<PartResponseDto>> GetAllPartsAsync();

        Task<PartResponseDto?> GetPartByIdAsync(int id);

        Task<PartResponseDto?> UpdatePartAsync(int id, UpdatePartDto dto);

        Task<bool> DeletePartAsync(int id);
    }
}