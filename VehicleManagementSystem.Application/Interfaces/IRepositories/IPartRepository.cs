using VehicleManagementSystem.Domain.Models;
namespace VehicleManagementSystem.Application.Interfaces.IRepositories
{
    public interface IPartRepository
    {
        Task<Part> AddAsync(Part part);
        Task<List<Part>> GetAllAsync();
        Task<Part?> GetByIdAsync(int id);
        Task<Part?> UpdateAsync(int id, Part part);
        Task<bool> DeleteAsync(int id);
        Task<List<Part>> GetByVendorIdAsync(int vendorId);
        Task<Part?> GetByNameAsync(string partName);
    }
}