using VehicleManagementSystem.Domain.Models;

namespace VehicleManagementSystem.Application.Interfaces.IRepositories
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomerAsync(Customer customer);

        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);

        Task<Customer?> GetCustomerWithVehiclesAsync(int customerId);
    }
}