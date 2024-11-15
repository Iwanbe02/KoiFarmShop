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
        public DashBoardController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetMonthlyOrders")]
        public async Task<IActionResult> GetMonthlyOrders()
        {
            var monthlyConsignments = await _orderService.GetMonthlyOrders();
            return Ok(monthlyConsignments);
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
       
    }
}
