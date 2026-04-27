using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Infrastructure.Persistence;
using VehicleManagementSystem.Domain.Models;

namespace VehicleManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateStaff([FromBody] Staff staff)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Staffs.Add(staff);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetStaffById), new { id = staff.Id }, staff);
        }

        [HttpGet]
        public IActionResult GetAllStaff()
        {
            return Ok(_context.Staffs.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetStaffById(int id)
        {
            var staff = _context.Staffs.Find(id);

            if (staff == null)
                return NotFound();

            return Ok(staff);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStaff(int id, Staff updatedStaff)
        {
            if (id != updatedStaff.Id)
                return BadRequest();

            var staff = _context.Staffs.Find(id);

            if (staff == null)
                return NotFound();

            staff.FullName = updatedStaff.FullName;
            staff.Email = updatedStaff.Email;
            staff.Password = updatedStaff.Password;
            staff.Role = updatedStaff.Role;
            staff.PhoneNumber = updatedStaff.PhoneNumber;

            _context.SaveChanges();

            return Ok(staff);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStaff(int id)
        {
            var staff = _context.Staffs.Find(id);

            if (staff == null)
                return NotFound();

            _context.Staffs.Remove(staff);
            _context.SaveChanges();

            return Ok("Deleted successfully");
        }
    }
}