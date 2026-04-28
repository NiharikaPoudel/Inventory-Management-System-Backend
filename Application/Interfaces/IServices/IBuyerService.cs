using Autolaya.Application.DTOs.Buyer;

namespace Autolaya.Application.Interfaces.IServices;

public interface IBuyerService
{
    Task<CustomerDto?> GetProfileAsync(string userId);
    Task<bool> UpdateProfileAsync(string userId, UpdateCustomerDto dto);
    Task<List<VehicleDto>> GetGarageAsync(string userId);
    Task<List<AppointmentDto>> GetAppointmentsAsync(string userId);
    Task<(bool Success, string Message)> BookAppointmentAsync(string userId, CreateAppointmentDto dto);
    Task<List<ServiceHistoryDto>> GetServiceHistoryAsync(string userId);
    Task<(bool Success, string Message)> SubmitReviewAsync(string userId, CreateReviewDto dto);
    Task<List<ReviewDto>> GetPendingReviewsAsync(string userId);
    Task<List<ReviewDto>> GetFeaturedReviewsAsync();
    Task<(bool Success, string Message)> SubmitPartRequestAsync(string userId, CreatePartRequestDto dto);
    Task<List<PartRequestDto>> GetPartRequestsAsync(string userId);
    Task<object> GetDashboardSummaryAsync(string userId);
}