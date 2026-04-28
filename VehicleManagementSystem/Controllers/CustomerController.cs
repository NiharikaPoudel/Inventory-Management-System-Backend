using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.DTOs.Customer;

namespace VehicleManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        // ─── Feature 12: POST api/customer/register ───────────────────────────

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.RegisterCustomerAsync(dto);
            return CreatedAtAction(nameof(GetProfile), new { id = result.Id }, result);
        }

        // ─── Feature 12: PUT api/customer/{id}/profile ────────────────────────

        [HttpPut("{id}/profile")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.UpdateProfileAsync(id, dto);

            if (result == null)
                return NotFound("Customer not found.");

            return Ok(result);
        }

        // ─── Feature 8: GET api/customer/{id}/profile ────────────────────────

        [HttpGet("{id}/profile")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var result = await _service.GetCustomerProfileAsync(id);

            if (result == null)
                return NotFound("Customer not found.");

            return Ok(result);
        }

        // ─── Feature 8: GET api/customer/{id}/details ────────────────────────

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetCustomerWithVehicles(int id)
        {
            var result = await _service.GetCustomerWithVehiclesAsync(id);

            if (result == null)
                return NotFound("Customer not found.");

            return Ok(result);
        }
    }
}



//using Microsoft.AspNetCore.Mvc;
//using VehicleManagementSystem.Application.Interfaces.IServices;
//using VehicleManagementSystem.DTOs.Customer;

//namespace VehicleManagementSystem.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CustomerController : ControllerBase
//    {
//        private readonly ICustomerService _service;

//        public CustomerController(ICustomerService service)
//        {
//            _service = service;
//        }

//        // POST: register customer + vehicle
//        [HttpPost("register-with-vehicle")]
//        public async Task<IActionResult> RegisterCustomerWithVehicle([FromBody] CreateCustomerWithVehicleDto dto)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);

//            var result = await _service.RegisterCustomerWithVehicleAsync(dto);
//            return Ok(result);
//        }

//        // GET: customer with vehicles
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetCustomerWithVehicles(int id)
//        {
//            var customer = await _service.GetCustomerWithVehiclesAsync(id);

//            if (customer == null)
//                return NotFound("Customer not found");

//            return Ok(customer);
//        }
//    }
//}