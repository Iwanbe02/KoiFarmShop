using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishDTO;
using DataAccessObjects.DTOs.PaymentDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace KoiFarmShop_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
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
        public async Task<IActionResult> DeleteKoiFish(int Id)
        {
            var payment = await _paymentService.DeletePayment(Id);
            return Ok();
        }

    }
}
