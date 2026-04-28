using VehicleManagementSystem.Domain.Models;

namespace VehicleManagementSystem.Application.Interfaces.IRepositories
{
    // ─── Feature 8 + 12 ──────────────────────────────────────────────────────

    public interface ICustomerRepository
    {
        // Feature 12: Registration
        Task<Customer> AddCustomerAsync(Customer customer);

        // Feature 12: Update profile
        Task<Customer?> GetByIdAsync(int customerId);
        Task<Customer> UpdateCustomerAsync(Customer customer);

        // Feature 8: Detail + vehicles
        Task<Customer?> GetCustomerWithVehiclesAsync(int customerId);

        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
    }
}



//using VehicleManagementSystem.Domain.Models;

//namespace VehicleManagementSystem.Application.Interfaces.IRepositories
//{
//    public interface ICustomerRepository
//    {
//        Task<Customer> AddCustomerAsync(Customer customer);

//        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);

//        Task<Customer?> GetCustomerWithVehiclesAsync(int customerId);
//    }
//}