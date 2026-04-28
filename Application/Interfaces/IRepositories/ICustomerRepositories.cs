using Autolaya.Domain.Models;

namespace Autolaya.Application.Interfaces.IRepositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByUserIdAsync(string userId);
    Task<Customer?> GetByIdAsync(int id);
    Task UpdateAsync(Customer customer);
    Task<List<Vehicle>> GetVehiclesByCustomerIdAsync(int customerId);
    Task<List<Appointment>> GetAppointmentsByCustomerIdAsync(int customerId);
    Task<List<ServiceHistory>> GetServiceHistoryByCustomerIdAsync(int customerId);
    Task<List<Review>> GetFeaturedReviewsAsync();
    Task<int> GetAppointmentCountByDateAsync(DateTime date);
    Task<Appointment> AddAppointmentAsync(Appointment appointment);
    Task<PartRequest> AddPartRequestAsync(PartRequest partRequest);
    Task<List<PartRequest>> GetPartRequestsByCustomerIdAsync(int customerId);
    Task<Review> AddReviewAsync(Review review);
    Task<ServiceHistory?> GetServiceHistoryByIdAsync(int id);
}