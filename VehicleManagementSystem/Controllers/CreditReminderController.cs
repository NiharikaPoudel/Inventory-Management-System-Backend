using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Application.Interfaces.IServices;

namespace VehicleManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditReminderController : ControllerBase
    {
        private readonly ICreditReminderService _service;

        public CreditReminderController(ICreditReminderService service)
        {
            _service = service;
        }

        [HttpGet("unpaid")]
        public async Task<IActionResult> GetUnpaidCredits()
        {
            var result = await _service.GetUnpaidCreditsAsync();

            return Ok(result);
        }

        [HttpPost("send/{saleId}")]
        public async Task<IActionResult> SendReminder(int saleId)
        {
            var result = await _service.SendReminderAsync(saleId);

            if (!result)
                return BadRequest("Unable to send reminder. Sale, customer email, or unpaid amount may be invalid.");

            return Ok("Reminder email sent successfully.");
        }
    }
}
