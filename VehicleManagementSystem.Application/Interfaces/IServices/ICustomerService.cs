using VehicleManagementSystem.DTOs.Customer;

namespace VehicleManagementSystem.Application.Interfaces.IServices
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> RegisterCustomerWithVehicleAsync(CreateCustomerWithVehicleDto dto);

        Task<CustomerResponseDto?> GetCustomerWithVehiclesAsync(int customerId);
    }
}