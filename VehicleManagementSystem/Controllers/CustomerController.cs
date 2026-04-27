using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Domain.Models;
using VehicleManagementSystem.Infrastructure.Persistence;
using VehicleManagementSystem.DTOs;

namespace VehicleManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register-with-vehicle")]
        public IActionResult RegisterCustomerWithVehicle(RegisterCustomerWithVehicleDto dto)
        {
            var customer = new Customer
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            _context.Customers.Add(customer);
            _context.SaveChanges();

            var vehicle = new Vehicle
            {
                VehicleNumber = dto.VehicleNumber,
                Make = dto.Make,
                Model = dto.Model,
                Year = dto.Year,
                Color = dto.Color,
                CustomerId = customer.Id
            };

            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Customer and Vehicle registered successfully",
                customer = new
                {
                    customer.Id,
                    customer.FullName,
                    customer.Email,
                    customer.Phone,
                    customer.Address,
                    customer.RegisteredAt
                },
                vehicle = new
                {
                    vehicle.Id,
                    vehicle.VehicleNumber,
                    vehicle.Make,
                    vehicle.Model,
                    vehicle.Year,
                    vehicle.Color,
                    vehicle.CustomerId
                }
            });
        }
    }
}