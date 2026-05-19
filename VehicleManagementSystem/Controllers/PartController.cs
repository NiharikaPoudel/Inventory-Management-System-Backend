using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.DTOs.Part;

namespace VehicleManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartController : ControllerBase
    {
        private readonly IPartService _service;

        public PartController(IPartService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePart(CreatePartDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.CreatePartAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParts()
        {
            var result = await _service.GetAllPartsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPartById(int id)
        {
            var result = await _service.GetPartByIdAsync(id);
            if (result == null) return NotFound("Part not found.");
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePart(int id, UpdatePartDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _service.UpdatePartAsync(id, dto);
            if (result == null) return NotFound("Part not found.");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePart(int id)
        {
            var result = await _service.DeletePartAsync(id);
            if (!result) return NotFound("Part not found.");
            return Ok("Part deleted successfully.");
        }

        [HttpGet("by-vendor/{vendorId}")]
        public async Task<IActionResult> GetPartsByVendor(int vendorId)
        {
            var result = await _service.GetPartsByVendorAsync(vendorId);
            return Ok(result);
        }
    }
}
