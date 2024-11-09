using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IConsignmentService _consignmentService;
        public DashBoardController(IOrderService orderService, IConsignmentService consignmentService)
        {
            _orderService = orderService;
            _consignmentService = consignmentService;

        }

        [HttpGet("GetMonthlyOrders")]
        public async Task<IActionResult> GetMonthlyOrders()
        {
            var monthlyOrders = await _orderService.GetMonthlyOrders();
            return Ok(monthlyOrders);
        }

        [HttpGet("GetTotalPriceOrders")]
        public async Task<IActionResult> GetTotalPriceOrders()
        {
            var order = await _orderService.GetTotalPriceOrders();
            return Ok(order);
        }

        [HttpGet("TotalOrders/{month}")]
        public async Task<IActionResult> GetTotalDonationsByYear(int month)
        {
            var totalDonations = await _orderService.GetTotalOrdersByMonth(month);
            return Ok(totalDonations);
        }

        [HttpGet("GetMonthlyConsignments")]
        public async Task<IActionResult> GetMonthlyConsignments()
        {
            var monthlyConsignments = await _consignmentService.GetMonthlyConsignments();
            return Ok(monthlyConsignments);
        }     

        [HttpGet("GetTotalPriceConsignments")]
        public async Task<IActionResult> GetTotalPriceConsignments()
        {
            var consignment = await _consignmentService.GetTotalPriceConsignments();
            return Ok(consignment);
        }

        [HttpGet("TotalConsignments/{month}")]
        public async Task<IActionResult> GetTotalConsignmentsByMonth(int month)
        {
            var totalConsignments = await _consignmentService.GetTotalConsignmentsByMonth(month);
            return Ok(totalConsignments);
        }

        
    }
}
