using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Domain.Models;
using VehicleManagementSystem.Infrastructure.Persistence;

namespace VehicleManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VendorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateVendor([FromBody] Vendor vendor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Vendors.Add(vendor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetVendorById), new { id = vendor.Id }, vendor);
        }

        [HttpGet]
        public IActionResult GetAllVendors()
        {
            return Ok(_context.Vendors.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetVendorById(int id)
        {
            var vendor = _context.Vendors.Find(id);

            if (vendor == null)
                return NotFound();

            return Ok(vendor);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVendor(int id, Vendor updatedVendor)
        {
            if (id != updatedVendor.Id)
                return BadRequest();

            var vendor = _context.Vendors.Find(id);

            if (vendor == null)
                return NotFound();

            vendor.Name = updatedVendor.Name;
            vendor.ContactPerson = updatedVendor.ContactPerson;
            vendor.Email = updatedVendor.Email;
            vendor.Phone = updatedVendor.Phone;
            vendor.Address = updatedVendor.Address;

            _context.SaveChanges();

            return Ok(vendor);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVendor(int id)
        {
            var vendor = _context.Vendors.Find(id);

            if (vendor == null)
                return NotFound();

            _context.Vendors.Remove(vendor);
            _context.SaveChanges();

            return Ok("Vendor deleted successfully");
        }
    }
}