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
                KoiId = createOrder.KoiId,
                KoiFishyId = createOrder.KoiFishyId,
                ConsignmentId = createOrder.ConsignmentId,
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

        public async Task<Dictionary<int, Dictionary<string, Dictionary<int, decimal>>>> GetMonthlyKoiSales()
        {
            var orders = await _orderRepository.GetAllAsync();

            var monthlyKoiSales = orders
                .Where(order => order.KoiId.HasValue && order.Status == OrderStatus.Paid.ToString())  
                .GroupBy(d => new { d.CreatedDate.Year, d.CreatedDate.Month, d.KoiId })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    KoiId = g.Key.KoiId.Value,  
                    TotalPrice = g.Sum(d => d.Price)
                });

            var result = new Dictionary<int, Dictionary<string, Dictionary<int, decimal>>>();

            foreach (var item in monthlyKoiSales)
            {
                if (!result.ContainsKey(item.Year))
                {
                    result[item.Year] = new Dictionary<string, Dictionary<int, decimal>>();
                }

                string monthName = new DateTime(item.Year, item.Month, 1).ToString("MMMM");

                if (!result[item.Year].ContainsKey(monthName))
                {
                    result[item.Year][monthName] = new Dictionary<int, decimal>();
                }

                result[item.Year][monthName][item.KoiId] = item.TotalPrice;
            }

            foreach (var year in result.Keys)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthName = new DateTime(year, month, 1).ToString("MMMM");
                    if (!result[year].ContainsKey(monthName))
                    {
                        result[year][monthName] = new Dictionary<int, decimal>();
                    }
                }
            }

            return result;
        }

        public async Task<Dictionary<int, Dictionary<string, Dictionary<int, decimal>>>> GetMonthlyKoiFishySales()
        {
            var orders = await _orderRepository.GetAllAsync();

            var monthlyKoiSales = orders
                .Where(order => order.KoiFishyId.HasValue && order.Status == OrderStatus.Paid.ToString())
                .GroupBy(d => new { d.CreatedDate.Year, d.CreatedDate.Month, d.KoiFishyId })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    KoiFishyId = g.Key.KoiFishyId.Value,
                    TotalPrice = g.Sum(d => d.Price)
                });

            var result = new Dictionary<int, Dictionary<string, Dictionary<int, decimal>>>();

            foreach (var item in monthlyKoiSales)
            {
                if (!result.ContainsKey(item.Year))
                {
                    result[item.Year] = new Dictionary<string, Dictionary<int, decimal>>();
                }

                string monthName = new DateTime(item.Year, item.Month, 1).ToString("MMMM");

                if (!result[item.Year].ContainsKey(monthName))
                {
                    result[item.Year][monthName] = new Dictionary<int, decimal>();
                }

                result[item.Year][monthName][item.KoiFishyId] = item.TotalPrice;
            }

            foreach (var year in result.Keys)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthName = new DateTime(year, month, 1).ToString("MMMM");
                    if (!result[year].ContainsKey(monthName))
                    {
                        result[year][monthName] = new Dictionary<int, decimal>();
                    }
                }
            }

            return result;
        }

        public async Task<Dictionary<int, Dictionary<string, Dictionary<int, decimal>>>> GetMonthlyConsignment()
        {
            var orders = await _orderRepository.GetAllAsync();

            var monthlyKoiSales = orders
                .Where(order => order.ConsignmentId.HasValue && order.Status == OrderStatus.Paid.ToString())
                .GroupBy(d => new { d.CreatedDate.Year, d.CreatedDate.Month, d.ConsignmentId })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    ConsignmentId = g.Key.ConsignmentId.Value,
                    TotalPrice = g.Sum(d => d.Price)
                });

            var result = new Dictionary<int, Dictionary<string, Dictionary<int, decimal>>>();

            foreach (var item in monthlyKoiSales)
            {
                if (!result.ContainsKey(item.Year))
                {
                    result[item.Year] = new Dictionary<string, Dictionary<int, decimal>>();
                }

                string monthName = new DateTime(item.Year, item.Month, 1).ToString("MMMM");

                if (!result[item.Year].ContainsKey(monthName))
                {
                    result[item.Year][monthName] = new Dictionary<int, decimal>();
                }

                result[item.Year][monthName][item.ConsignmentId] = item.TotalPrice;
            }

            foreach (var year in result.Keys)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthName = new DateTime(year, month, 1).ToString("MMMM");
                    if (!result[year].ContainsKey(monthName))
                    {
                        result[year][monthName] = new Dictionary<int, decimal>();
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
            var totalOrders = orders
                .Where(o => o.Status == OrderStatus.Paid.ToString() && o.CreatedDate.Month == month)
                .Count();

            return totalOrders;
        }

        public async Task<decimal> GetTotalPriceOrders()
        {
            var orderPrice = await _orderRepository.GetAllAsync();

            var totalPrice = orderPrice
                .Where(o => o.Status == OrderStatus.Paid.ToString())
                .Sum(o => o.Price);

            return totalPrice;
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
