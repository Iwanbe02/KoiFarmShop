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
        private readonly IKoiFishService _koiFishService;
        private readonly IKoiFishyService _koiFishyService;
        public DashBoardController(IOrderService orderService, IConsignmentService consignmentService, IKoiFishService koiFishService, IKoiFishyService koiFishyService)
        {
            _orderService = orderService;
            _consignmentService = consignmentService;
            _koiFishService = koiFishService;
            _koiFishyService = koiFishyService;
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
        public async Task<IActionResult> GetTotalOrdersByMonth(int month)
        {
            var totalOrders = await _orderService.GetTotalOrdersByMonth(month);
            return Ok(totalOrders);
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
        public async Task<IActionResult> GetTotaConsignmentsByMonth(int month)
        {
            var totalConsignments = await _consignmentService.GetTotaConsignmentsByMonth(month);
            return Ok(totalConsignments);
        }

        [HttpGet("GetMonthlyKoiFish")]
        public async Task<IActionResult> GetMonthlyKoiFish()
        {
            var monthlyKoiFish = await _koiFishService.GetMonthlyKoiFish();
            return Ok(monthlyKoiFish);
        }

        [HttpGet("GetTotalPriceKoiFish")]
        public async Task<IActionResult> GetTotalPriceKoiFish()
        {
            var koi = await _koiFishService.GetTotalPriceKoiFish();
            return Ok(koi);
        }

        [HttpGet("TotalKoiFish/{month}")]
        public async Task<IActionResult> GetTotalKoiFishByMonth(int month)
        {
            var totalKoi = await _koiFishService.GetTotalKoiFishByMonth(month);
            return Ok(totalKoi);
        }

        [HttpGet("GetMonthlyKoiFishy")]
        public async Task<IActionResult> GetMonthlyKoiFishy()
        {
            var monthlyKoiFishy = await _koiFishyService.GetMonthlyKoiFishy();
            return Ok(monthlyKoiFishy);
        }

        [HttpGet("GetTotalPriceKoiFishy")]
        public async Task<IActionResult> GetTotalPriceKoiFishy()
        {
            var koi = await _koiFishyService.GetTotalPriceKoiFishy();
            return Ok(koi);
        }

        [HttpGet("TotalKoiFishy/{month}")]
        public async Task<IActionResult> GetTotalKoiFishyByMonth(int month)
        {
            var totalKoi = await _koiFishyService.GetTotalKoiFishyByMonth(month);
            return Ok(totalKoi);
        }
    }
}
