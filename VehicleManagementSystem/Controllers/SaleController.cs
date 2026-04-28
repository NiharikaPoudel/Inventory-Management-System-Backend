//using Microsoft.AspNetCore.Mvc;
//using VehicleManagementSystem.Application.Interfaces.IServices;

//namespace VehicleManagementSystem.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SaleController : ControllerBase
//    {
//        private readonly ISaleService _service;

//        public SaleController(ISaleService service)
//        {
//            _service = service;
//        }

//        // ─── Feature 14: GET api/sale/customer/{customerId}/history ──────────

//        [HttpGet("customer/{customerId}/history")]
//        public async Task<IActionResult> GetSaleHistory(int customerId)
//        {
//            var result = await _service.GetSaleHistoryByCustomerAsync(customerId);
//            return Ok(result);
//        }

//        // ─── Feature 14: GET api/sale/{id} ───────────────────────────────────

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetSaleDetail(int id)
//        {
//            var result = await _service.GetSaleDetailAsync(id);

//            if (result == null)
//                return NotFound("Sale record not found.");

//            return Ok(result);
//        }
//    }
//}


////using Microsoft.AspNetCore.Mvc;
////using VehicleManagementSystem.Application.Interfaces.IServices;
////using VehicleManagementSystem.DTOs.Sale;

////namespace VehicleManagementSystem.Controllers
////{
////    [Route("api/[controller]")]
////    [ApiController]
////    public class SaleController : ControllerBase
////    {
////        private readonly ISaleService _service;

////        public SaleController(ISaleService service)
////        {
////            _service = service;
////        }

////        [HttpPost]
////        public async Task<IActionResult> CreateSale(CreateSaleDto dto)
////        {
////            var result = await _service.CreateSaleAsync(dto);
////            return Ok(result);
////        }

////        [HttpGet]
////        public async Task<IActionResult> GetAllSales()
////        {
////            var result = await _service.GetAllSalesAsync();
////            return Ok(result);
////        }

////        [HttpGet("{id}")]
////        public async Task<IActionResult> GetSaleById(int id)
////        {
////            var result = await _service.GetSaleByIdAsync(id);

////            if (result == null)
////                return NotFound("Sale not found");

////            return Ok(result);
////        }
////    }
////}