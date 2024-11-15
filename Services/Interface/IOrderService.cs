using BusinessObjects.Models;
using DataAccessObjects.DTOs.KoiFishDTO;
using DataAccessObjects.DTOs.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<Order> CreateOrder(CreateOrderDTO createOrder);
        Task<Order> UpdateOrder(int id, UpdateOrderDTO updateOrder);
        Task<Order> DeleteOrder(int id);
        Task<Order> RestoreOrder(int id);
        Task<Dictionary<int, Dictionary<string, decimal>>> GetMonthlyOrders();
        Task<decimal> GetTotalPriceOrders();
        Task<int> GetTotalOrdersByMonth(int month);
    }
}
