using BusinessObjects.Models;
using DataAccessObjects.DTOs.PaymentDTO;
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
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<Payment> CreatePayment(CreatePaymentDTO createPayment)
        {
            var payment = new Payment
            {
                PaymentMethod = createPayment.PaymentMethod,
                Status = createPayment.Status,
            };
            await _paymentRepository.AddAsync(payment);
            return payment;
        }

        public async Task<Payment> DeletePayment(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
            {
                throw new Exception($"Payment with ID{id} is not found");
            }
            payment.IsDeleted = true;
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
            payment.Status = updatePayment.Status;
            await _paymentRepository.UpdateAsync(payment);
            return payment;
        }
    }
}
