using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Application.Interfaces.IRepositories;
using VehicleManagementSystem.Domain.Models;
using VehicleManagementSystem.Infrastructure.Persistence;

namespace VehicleManagementSystem.Infrastructure.Repositories
{
    public class PartRepository : IPartRepository
    {
        private readonly ApplicationDbContext _context;

        public PartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Part> AddAsync(Part part)
        {
            _context.Parts.Add(part);
            await _context.SaveChangesAsync();
            return part;
        }

        public async Task<List<Part>> GetAllAsync()
        {
            return await _context.Parts.ToListAsync();
        }

        public async Task<Part?> GetByIdAsync(int id)
        {
            return await _context.Parts.FindAsync(id);
        }

        public async Task<Part?> GetByNameAsync(string partName)
        {
            return await _context.Parts
                .FirstOrDefaultAsync(p => p.PartName.ToLower() == partName.ToLower());
        }

        public async Task<Part?> UpdateAsync(int id, Part updatedPart)
        {
            var existingPart = await _context.Parts.FindAsync(id);
            if (existingPart == null) return null;

            existingPart.PartName = updatedPart.PartName;
            existingPart.PartNumber = updatedPart.PartNumber;
            existingPart.Category = updatedPart.Category;
            existingPart.Quantity = updatedPart.Quantity;
            existingPart.PurchasePrice = updatedPart.PurchasePrice;
            existingPart.SellingPrice = updatedPart.SellingPrice;
            existingPart.VendorId = updatedPart.VendorId;

            await _context.SaveChangesAsync();
            return existingPart;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var part = await _context.Parts.FindAsync(id);
            if (part == null) return false;
            _context.Parts.Remove(part);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Part>> GetByVendorIdAsync(int vendorId)
        {
            return await _context.Parts
                .Where(p => p.VendorId == vendorId)
                .ToListAsync();
        }
    }
}