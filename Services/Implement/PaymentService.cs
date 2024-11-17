using BusinessObjects.Enums;
using BusinessObjects.Helpers;
using BusinessObjects.Models;
using DataAccessObjects.DTOs.PaymentDTO;
using Microsoft.Extensions.Configuration;
using Repositories.Implement;
using Repositories.Interface;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IKoiFishService _koiFishService;
        private readonly IKoiFishyService _koiFishyService;
        private readonly IConsignmentService _consignmentService;
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;
        public PaymentService(IPaymentRepository paymentRepository, IConfiguration configuration, IOrderRepository orderRepository, ICartItemRepository cartItemRepository, IKoiFishService koiFishService, IKoiFishyService koiFishyService, IConsignmentService consignmentService)
        {
            _paymentRepository = paymentRepository;
            _configuration = configuration;
            _orderRepository = orderRepository;
            _cartItemRepository = cartItemRepository;
            _koiFishService = koiFishService;
            _koiFishyService = koiFishyService;
            _consignmentService = consignmentService;
        }

        public async Task<string> CreatePaymentAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null || order.Price == null)
                throw new ArgumentException("Order not found or has no price");

            var payment = new Payment
            {
                Amount = order.Price,
                PaymentMethod = "VNPAY", 
                CreatedDate = DateTime.Now
            };

            await _paymentRepository.AddAsync(payment);

            var vnPay = new VnPayLibrary();
            vnPay.AddRequestData("vnp_Version", "2.1.0");
            vnPay.AddRequestData("vnp_Command", "pay");
            vnPay.AddRequestData("vnp_TmnCode", _configuration["VNPay:TmnCode"]);
            vnPay.AddRequestData("vnp_Amount", ((int)(payment.Amount * 100)).ToString()); 
            vnPay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPay.AddRequestData("vnp_CurrCode", "VND");
            vnPay.AddRequestData("vnp_IpAddr", "123.21.100.96"); 
            vnPay.AddRequestData("vnp_Locale", "vn");
            vnPay.AddRequestData("vnp_OrderInfo", $"Payment for order {orderId}");
            vnPay.AddRequestData("vnp_OrderType", "billpayment");
            vnPay.AddRequestData("vnp_ReturnUrl", _configuration["VNPay:ReturnUrl"]);

            string uniqueTxnRef = $"{orderId}_{DateTime.Now.Ticks}";
            vnPay.AddRequestData("vnp_TxnRef", uniqueTxnRef);

            string vnpBaseUrl = _configuration["VNPay:Url"];
            string vnpHashSecret = _configuration["VNPay:HashSecret"];
            string paymentUrl = vnPay.CreateRequestUrl(vnpBaseUrl, vnpHashSecret);

            order.Status = OrderStatus.Pending.ToString();
            order.PaymentId = payment.Id; 
            await _orderRepository.UpdateAsync(order);

            return paymentUrl;
        }

        public async Task ProcessPaymentAsync(int orderId, string responseCode)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            if (responseCode == "00") 
            {
                order.Status = OrderStatus.Paid.ToString();
                await _orderRepository.UpdateAsync(order);

                var cartItems = await _cartItemRepository.GetByOrderIdAsync(order.Id);  
                foreach (var cartItem in cartItems)
                {
                    if (cartItem.KoiFishId.HasValue)
                    {
                        await _koiFishService.UpdateKoiFishStatus(cartItem.KoiFishId.Value, KoiFishStatus.Sold.ToString());
                    }
                    if (cartItem.KoiFishyId.HasValue)
                    {
                        await _koiFishyService.UpdateKoiFishyStatus(cartItem.KoiFishyId.Value, KoiFishStatus.Sold.ToString());
                    }
                    if (cartItem.ConsignmentId.HasValue)
                    {
                        await _consignmentService.UpdateConsignmentStatus(cartItem.ConsignmentId.Value, OrderStatus.Paid.ToString());
                    }
                }
            }
            else 
            {
                order.Status = OrderStatus.Cancelled.ToString();
                await _orderRepository.UpdateAsync(order);
            }
        }

        public async Task<Payment> DeletePayment(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                throw new Exception($"Payment with ID{id} is not found");
            }
            payment.IsDeleted = true;
            payment.DeletedDate = DateTime.Now;
            await _paymentRepository.UpdateAsync(payment);
            return payment;
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
        {
            return await _paymentRepository.GetAllAsync();
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _paymentRepository.GetByIdAsync(id);
        }

        public async Task<Payment> RestorePayment(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                throw new Exception($"Payment with ID{id} is not found");
            }
            if (payment.IsDeleted == true)
            {
                payment.IsDeleted = false;
                await _paymentRepository.UpdateAsync(payment);
            }
            return payment;
        }

        public async Task<Payment> UpdatePayment(int id, UpdatePaymentDTO updatePayment)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                throw new Exception($"Payment with ID{id} is not found");
            }
            payment.PaymentMethod = updatePayment.PaymentMethod;
            payment.ModifiedDate = DateTime.Now;
            await _paymentRepository.UpdateAsync(payment);
            return payment;
        }
    }
}
