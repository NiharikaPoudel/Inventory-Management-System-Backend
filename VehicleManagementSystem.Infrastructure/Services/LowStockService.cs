using Microsoft.EntityFrameworkCore;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.DTOs.Notification;
using VehicleManagementSystem.Infrastructure.Persistence;

namespace VehicleManagementSystem.Infrastructure.Services
{
    public class LowStockService : ILowStockService
    {
        private readonly ApplicationDbContext _context;

        public LowStockService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LowStockNotificationDto>> GetLowStockNotificationsAsync()
        {
            return await _context.Parts
                .Where(p => p.Quantity < 10)
                .OrderBy(p => p.Quantity)
                .Select(p => new LowStockNotificationDto
                {
                    PartId = p.Id,
                    PartName = p.PartName,
                    PartNumber = p.PartNumber,
                    Category = p.Category,
                    Quantity = p.Quantity,
                    Message = $"Low stock alert: {p.PartName} has only {p.Quantity} item(s) remaining."
                })
                .ToListAsync();
        }
    }
}
