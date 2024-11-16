using BusinessObjects.Enums;
using BusinessObjects.Helpers;
using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishDTO;
using DataAccessObjects.DTOs.PaymentDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Implement;
using Repositories.Interface;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;
        public PaymentController(IPaymentService paymentService, IOrderRepository orderRepository, IConfiguration configuration)
        {
            _paymentService = paymentService;
            _orderRepository = orderRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payment = await _paymentService.GetAllPayments();
            return Ok(payment);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPaymentById(int Id)
        {
            var payment = await _paymentService.GetPaymentById(Id);
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(int orderId)
        {
            var paymentUrl = await _paymentService.CreatePaymentAsync(orderId);
            return Ok(new { PaymentUrl = paymentUrl });
        }

        [HttpPost("return")]
        public async Task<IActionResult> Return([FromQuery] int vnp_TxnRef, [FromQuery] string vnp_ResponseCode, [FromQuery] string vnp_SecureHash)
        {
            try
            {
                await _paymentService.ProcessPaymentAsync(vnp_TxnRef, vnp_ResponseCode);

                return Ok(new
                {
                    success = vnp_ResponseCode == "00",
                    message = vnp_ResponseCode == "00" ? "Payment successful." : "Payment failed.",
                    OrderId = vnp_TxnRef,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdatePayment(int Id, UpdatePaymentDTO updatePaymentDTO)
        {
            var payment = await _paymentService.UpdatePayment(Id, updatePaymentDTO);
            return Ok(payment);
        }

        [HttpPut("{Id}/{isDeleted}")]
        public async Task<IActionResult> RestorePayment(int Id)
        {
            await _paymentService.RestorePayment(Id);
            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePayment(int Id)
        {
            var payment = await _paymentService.DeletePayment(Id);
            return Ok();
        }

    }
}
