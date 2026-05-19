using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Application.Interfaces.IServices;

namespace VehicleManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyReport([FromQuery] DateTime date)
        {
            var result = await _service.GetDailyReportAsync(date);
            return Ok(result);
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyReport(
            [FromQuery] int year,
            [FromQuery] int month)
        {
            if (month < 1 || month > 12)
                return BadRequest("Month must be between 1 and 12.");

            var result = await _service.GetMonthlyReportAsync(year, month);
            return Ok(result);
        }

        [HttpGet("yearly")]
        public async Task<IActionResult> GetYearlyReport([FromQuery] int year)
        {
            var result = await _service.GetYearlyReportAsync(year);
            return Ok(result);
        }
    }
}