using System.Threading.Tasks;
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

        // ─── Feature: Booking Submission ─────────────────────────────────────
        [HttpPost("bookings")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookingId = await _service.CreateBookingAsync(dto);
            return Ok(new { message = "Booking submitted successfully", id = bookingId });
        }

        // ─── Feature: Request Parts ──────────────────────────────────────────
        [HttpPost("parts-request")]
        public async Task<IActionResult> RequestPart([FromBody] CreatePartRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var requestId = await _service.CreatePartRequestAsync(dto);
            return Ok(new { message = "Parts request submitted successfully", id = requestId });
        }

        // ─── Feature: Submit Service Review ──────────────────────────────────
        [HttpPost("reviews")]
        public async Task<IActionResult> SubmitReview([FromBody] SubmitReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _service.SubmitReviewAsync(dto);
            return Ok(new { message = "Review submitted successfully. Vendor rating updated." });
        }

        // ─── Feature: Homepage Featured Garages (Kisaan Bazaar Method) ───────
        // Filters vendors where Rating >= 4.5, max 6 slots dynamically
        [HttpGet("featured-vendors")]
        public async Task<IActionResult> GetFeaturedVendors()
        {
            var vendors = await _service.GetFeaturedVendorsAsync();
            return Ok(vendors);
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