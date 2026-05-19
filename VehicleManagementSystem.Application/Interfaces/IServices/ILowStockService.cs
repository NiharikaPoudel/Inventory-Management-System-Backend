using VehicleManagementSystem.DTOs.Notification;

namespace VehicleManagementSystem.Application.Interfaces.IServices
{
    public interface ILowStockService
    {
        Task<List<LowStockNotificationDto>> GetLowStockNotificationsAsync();
    }
}
