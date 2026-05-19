using VehicleManagementSystem.DTOs.CreditReminder;

namespace VehicleManagementSystem.Application.Interfaces.IServices
{
    public interface ICreditReminderService
    {
        Task<List<UnpaidCreditDto>> GetUnpaidCreditsAsync();
        Task<bool> SendReminderAsync(int saleId);
    }
}
