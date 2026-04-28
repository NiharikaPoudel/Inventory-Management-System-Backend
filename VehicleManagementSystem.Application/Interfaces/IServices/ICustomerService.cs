using VehicleManagementSystem.DTOs.Customer;

namespace VehicleManagementSystem.Application.Interfaces.IServices
{
    public interface ICustomerService
    {
        // Feature: Registration with Vehicle
        Task<CustomerResponseDto> RegisterCustomerWithVehicleAsync(CreateCustomerWithVehicleDto dto);

        // Feature 12: Individual Registration & Profile Management
        Task<CustomerProfileDto> RegisterCustomerAsync(RegisterCustomerDto dto);
        Task<CustomerProfileDto?> UpdateProfileAsync(int customerId, UpdateProfileDto dto);
        Task<CustomerProfileDto?> GetCustomerProfileAsync(int customerId);

        // Feature 8: Detail view with vehicles
        Task<CustomerResponseDto?> GetCustomerWithVehiclesAsync(int customerId);
    }
}