using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Application.Interfaces.IServices;

namespace VehicleManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LowStockController : ControllerBase
    {
        private readonly ILowStockService _service;

        public LowStockController(ILowStockService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetLowStockNotifications()
        {
            var result = await _service.GetLowStockNotificationsAsync();

            return Ok(result);
        }
    }
}
