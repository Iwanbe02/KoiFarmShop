using BusinessObjects.Models;
using DataAccessObjects.DTOs.CategoryDTO;
using DataAccessObjects.DTOs.PaymentDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPayments();
        Task ProcessPaymentAsync(int orderId, string responseCode);
        Task<Payment> GetPaymentById(int id);
        Task<string> CreatePaymentAsync(int orderId);
        Task<Payment> UpdatePayment(int id, UpdatePaymentDTO updatePayment);
        Task<Payment> DeletePayment(int id);
        Task<Payment> RestorePayment(int id);
    }
}
