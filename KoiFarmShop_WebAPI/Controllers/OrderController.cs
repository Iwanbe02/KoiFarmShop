using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishDTO;
using DataAccessObjects.DTOs.OrderDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var order = await _orderService.GetAllOrders();
            return Ok(order);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrderById(int Id)
        {
            var order = await _orderService.GetOrderById(Id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(CreateOrderDTO createOrderDTO)
        {
            var order = await _orderService.CreateOrder(createOrderDTO);
            return Ok(order);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateOrder(int Id, UpdateOrderDTO updateOrderDTO)
        {
            var order = await _orderService.UpdateOrder(Id, updateOrderDTO);
            return Ok(order);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestoreOrder(int Id)
        {
            await _orderService.RestoreOrder(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            var order = await _orderService.DeleteOrder(Id);
            return Ok();
        }
    }
}
