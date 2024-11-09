using BusinessObjects.Enums;
using BusinessObjects.Models;
using DataAccessObjects.DTOs.OrderDTO;
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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order> CreateOrder(CreateOrderDTO createOrder)
        {
            var order = new Order
            {
                CartId = createOrder.CartId,
                KoiId = createOrder.KoiId,
                KoiFishyId = createOrder.KoiFishyId,
                AccountId = createOrder.AccountId,
                PaymentId = createOrder.PaymentId,
                Status = OrderStatus.Pending.ToString(),
                Type = createOrder.Type,
                Price = createOrder.Price,
                CreatedDate = DateTime.Now
            };
            await _orderRepository.AddAsync(order);
            return order;
        }

        public async Task<Order> DeleteOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new Exception($"Order with ID{id} is not found");
            }
            order.IsDeleted = true;
            order.DeletedDate = DateTime.Now;
            await _orderRepository.UpdateAsync(order);
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Dictionary<int, Dictionary<string, decimal>>> GetMonthlyOrders()
        {
            var orders = await _orderRepository.GetAllAsync();

            // Group donations by year and month
            var monthlyOrders = orders
                .GroupBy(d => new { d.CreatedDate.Year, d.CreatedDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalPrice = g.Sum(d => d.Price)
                });

            // Create a nested dictionary to hold the total amounts for each month of each year
            var result = new Dictionary<int, Dictionary<string, decimal>>();
            foreach (var item in monthlyOrders)
            {
                if (!result.ContainsKey(item.Year))
                {
                    result[item.Year] = new Dictionary<string, decimal>();
                }

                string monthName = new DateTime(item.Year, item.Month, 1).ToString("MMMM");
                result[item.Year][monthName] = item.TotalPrice;
            }

            // Fill in months with zero for years that have no donations
            foreach (var year in result.Keys)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthName = new DateTime(year, month, 1).ToString("MMMM");
                    if (!result[year].ContainsKey(monthName))
                    {
                        result[year][monthName] = 0.00m; // Set to zero if no donations for that month
                    }
                }
            }

            return result;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<int> GetTotalOrdersByMonth(int month)
        {
            var orders = await _orderRepository.GetAllAsync();

            // Filter donations for the specified month and count them
            var totalOrders = orders.Count(d => d.CreatedDate.Month == month);

            return totalOrders;
        }

        public async Task<decimal> GetTotalPriceOrders()
        {
            var orderPrice = await _orderRepository.GetAllAsync();
            return orderPrice.Sum(o => o.Price);
        }

        public async Task<Order> RestoreOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new Exception($"Order with ID{id} is not found");
            }
            if (order.IsDeleted == true)
            {
                order.IsDeleted = false;
                await _orderRepository.UpdateAsync(order);
            }
            return order;
        }

        public async Task<Order> UpdateOrder(int id, UpdateOrderDTO updateOrder)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                throw new Exception($"Order with ID{id} is not found");
            }
            order.Status = updateOrder.Status;
            order.Type = updateOrder.Type;
            order.Price = updateOrder.Price;
            order.ModifiedDate = DateTime.Now;

            await _orderRepository.UpdateAsync(order);
            return order;
        }
    }
}
