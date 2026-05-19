using VehicleManagementSystem.Application.Interfaces.IRepositories;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.Domain.Models;
using VehicleManagementSystem.DTOs.Customer;

namespace VehicleManagementSystem.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        // --- Feature: Registration with Vehicle (The missing piece) ---
        public async Task<CustomerResponseDto> RegisterCustomerWithVehicleAsync(CreateCustomerWithVehicleDto dto)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            var createdCustomer = await _repository.AddCustomerAsync(customer);

            var vehicle = new Vehicle
            {
                VehicleNumber = dto.VehicleNumber,
                Make = dto.Make,
                Model = dto.Model,
                Year = dto.Year,
                Color = dto.Color,
                CustomerId = createdCustomer.Id
            };

            var createdVehicle = await _repository.AddVehicleAsync(vehicle);

            return new CustomerResponseDto
            {
                Id = createdCustomer.Id,
                FullName = createdCustomer.FullName,
                Email = createdCustomer.Email,
                Phone = createdCustomer.Phone,
                Address = createdCustomer.Address,
                RegisteredAt = createdCustomer.RegisteredAt,
                Vehicles = new List<VehicleResponseDto>
                {
                    new VehicleResponseDto
                    {
                        Id = createdVehicle.Id,
                        VehicleNumber = createdVehicle.VehicleNumber,
                        Make = createdVehicle.Make,
                        Model = createdVehicle.Model,
                        Year = createdVehicle.Year,
                        Color = createdVehicle.Color
                    }
                }
            };
        }

        // --- Feature 12: Individual Registration ---
        public async Task<CustomerProfileDto> RegisterCustomerAsync(RegisterCustomerDto dto)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            var created = await _repository.AddCustomerAsync(customer);
            return MapToProfileDto(created);
        }

        // --- Feature 12: Edit Profile ---
        public async Task<CustomerProfileDto?> UpdateProfileAsync(int customerId, UpdateProfileDto dto)
        {
            var customer = await _repository.GetByIdAsync(customerId);
            if (customer == null) return null;

            customer.FullName = dto.FullName;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.Address = dto.Address;

            var updated = await _repository.UpdateCustomerAsync(customer);
            return MapToProfileDto(updated);
        }

        // --- Feature 8: Detail view with vehicles ---
        public async Task<CustomerResponseDto?> GetCustomerWithVehiclesAsync(int customerId)
        {
            var customer = await _repository.GetCustomerWithVehiclesAsync(customerId);
            if (customer == null) return null;

            return new CustomerResponseDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                RegisteredAt = customer.RegisteredAt,
                Vehicles = customer.Vehicles.Select(v => new VehicleResponseDto
                {
                    Id = v.Id,
                    VehicleNumber = v.VehicleNumber,
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                    Color = v.Color
                }).ToList()
            };
        }

        // --- Feature 8: Profile only ---
        public async Task<CustomerProfileDto?> GetCustomerProfileAsync(int customerId)
        {
            var customer = await _repository.GetByIdAsync(customerId);
            if (customer == null) return null;

            return MapToProfileDto(customer);
        }

        // ─── NEW FEATURES: 4 Missing Methods ─────────────────────────────────

        // Feature: Create Booking
        public async Task<int> CreateBookingAsync(CreateBookingDto dto)
        {
            // TODO: Add booking logic here
            // For now, returning a dummy ID
            // You'll need to create Booking entity and repository method
            await Task.CompletedTask;
            return 1; // Placeholder booking ID
        }

        // Feature: Create Part Request
        public async Task<int> CreatePartRequestAsync(CreatePartRequestDto dto)
        {
            // TODO: Add part request logic here
            // For now, returning a dummy ID
            // You'll need to create PartRequest entity and repository method
            await Task.CompletedTask;
            return 1; // Placeholder part request ID
        }

        // Feature: Submit Review
        public async Task<bool> SubmitReviewAsync(SubmitReviewDto dto)
        {
            // TODO: Add review submission logic here
            // For now, returning true
            // You'll need to create Review entity and repository method
            await Task.CompletedTask;
            return true; // Placeholder success
        }

        // Feature: Get Featured Vendors (Homepage)
        public async Task<IEnumerable<FeaturedVendorDto>> GetFeaturedVendorsAsync()
        {
            // TODO: Add logic to fetch vendors with rating >= 4.5
            // For now, returning empty list
            // You'll need to add this method to your vendor repository
            await Task.CompletedTask;
            return new List<FeaturedVendorDto>(); // Placeholder empty list
        }

        // ─────────────────────────────────────────────────────────────────────

        private static CustomerProfileDto MapToProfileDto(Customer customer) => new()
        {
            Id = customer.Id,
            FullName = customer.FullName,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address,
            RegisteredAt = customer.RegisteredAt
        };
    }
}