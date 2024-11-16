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
                var payLib = new VnPayLibrary();

                var url = _configuration.GetValue<string>("VNPay:Url");
                var returnUrl = _configuration.GetValue<string>("VNPay:ReturnUrl");
                var tmnCode = _configuration.GetValue<string>("VNPay:TmnCode");
                var hashSecret = _configuration.GetValue<string>("VNPay:HashSecret");

                var order = await _orderRepository.GetByIdAsync(vnp_TxnRef);
                payLib.AddRequestData("vnp_Version", "2.1.0");
                payLib.AddRequestData("vnp_Command", "pay");
                payLib.AddRequestData("vnp_TmnCode", tmnCode);
                payLib.AddRequestData("vnp_Amount", (order.Price * 100).ToString()); 
                payLib.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                payLib.AddRequestData("vnp_CurrCode", "VND");
                payLib.AddRequestData("vnp_IpAddr", "123.21.100.96");
                payLib.AddRequestData("vnp_Locale", "vn");
                payLib.AddRequestData("vnp_OrderInfo", $"Thanh toán cho Donation {order.Id}");
                payLib.AddRequestData("vnp_OrderType", "donation");
                payLib.AddRequestData("vnp_ReturnUrl", returnUrl);
                payLib.AddRequestData("vnp_TxnRef", order.Id.ToString());
                payLib.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
                string paymentUrl = payLib.CreateRequestUrl(url, hashSecret);

                if (vnp_ResponseCode == "00") 
                {
                    order.Status = OrderStatus.Paid.ToString();
                    await _orderRepository.UpdateAsync(order);

                    return Ok(new
                    {
                        success = true,
                        message = "Payment successful.",
                        OrderId = order.Id,
                    });
                }
                else
                {
                    order.Status = OrderStatus.Cancelled.ToString();
                    await _orderRepository.UpdateAsync(order);

                    return Ok(new
                    {
                        success = false,
                        message = "Payment failed.",
                        OrderId = order.Id,
                    });
                }
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
