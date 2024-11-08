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
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;
        public PaymentService(IPaymentRepository paymentRepository, IConfiguration configuration, IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _configuration = configuration;
            _orderRepository = orderRepository;
        }

        public async Task<string> CreatePaymentAsync(int orderId)
        {
            // Step 1: Retrieve order details
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null || order.Price == null)
                throw new ArgumentException("Order not found or has no price");

            // Step 2: Create a new Payment object
            var payment = new Payment
            {
                Amount = order.Price,
                PaymentMethod = "VNPAY", // You can customize this field
                CreatedDate = DateTime.Now
            };

            await _paymentRepository.AddAsync(payment);

            // Step 3: Initialize VnPayLibrary and set request parameters
            var vnPay = new VnPayLibrary();
            vnPay.AddRequestData("vnp_Version", "2.1.0");
            vnPay.AddRequestData("vnp_Command", "pay");
            vnPay.AddRequestData("vnp_TmnCode", _configuration["VNPay:TmnCode"]);
            vnPay.AddRequestData("vnp_Amount", ((int)(payment.Amount * 100)).ToString()); // Convert to VND
            vnPay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPay.AddRequestData("vnp_CurrCode", "VND");
            vnPay.AddRequestData("vnp_IpAddr", "123.21.100.96"); // Replace with actual IP
            vnPay.AddRequestData("vnp_Locale", "vn");
            vnPay.AddRequestData("vnp_OrderInfo", $"Payment for order {orderId}");
            vnPay.AddRequestData("vnp_OrderType", "billpayment");
            vnPay.AddRequestData("vnp_ReturnUrl", _configuration["VNPay:ReturnUrl"]);
            vnPay.AddRequestData("vnp_TxnRef", orderId.ToString());

            // Step 4: Generate payment URL
            string vnpBaseUrl = _configuration["VNPay:Url"];
            string vnpHashSecret = _configuration["VNPay:HashSecret"];
            string paymentUrl = vnPay.CreateRequestUrl(vnpBaseUrl, vnpHashSecret);

            return paymentUrl;
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
