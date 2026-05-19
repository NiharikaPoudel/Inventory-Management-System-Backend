using Microsoft.AspNetCore.Mvc;
using VehicleManagementSystem.Application.Interfaces.IServices;
using VehicleManagementSystem.DTOs.Sale;

namespace VehicleManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _service;

        public SaleController(ISaleService service)
        {
            _service = service;
        }

        // POST: create sale
        [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateSaleAsync(dto);
            return Ok(result);
        }

        // GET: all sales
        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            var result = await _service.GetAllSalesAsync();
            return Ok(result);
        }

        // GET: single sale
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            var result = await _service.GetSaleByIdAsync(id);
            if (result == null)
                return NotFound("Sale not found");
            return Ok(result);
        }

        // GET: regular customers
        [HttpGet("reports/regulars")]
        public async Task<IActionResult> GetRegulars()
        {
            var result = await _service.GetRegularCustomersAsync();
            return Ok(result);
        }

        // GET: high spenders
        [HttpGet("reports/high-spenders")]
        public async Task<IActionResult> GetHighSpenders()
        {
            var result = await _service.GetHighSpendersAsync();
            return Ok(result);
        }

        // GET: pending credits
        [HttpGet("reports/pending-credits")]
        public async Task<IActionResult> GetPendingCredits()
        {
            var result = await _service.GetPendingCreditCustomersAsync();
            return Ok(result);
        }
    }
}