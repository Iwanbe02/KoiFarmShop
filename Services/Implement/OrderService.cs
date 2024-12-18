﻿using BusinessObjects.Enums;
using BusinessObjects.Models;
using DataAccessObjects.DTOs.OrderDTO;
using Microsoft.EntityFrameworkCore;
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
                AccountId = createOrder.AccountId,
                PaymentId = createOrder.PaymentId,
                Status = OrderStatus.Pending.ToString(),
                Address = createOrder.Address,
                Phone = createOrder.Phone,
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

        public async Task<IEnumerable<OrderDTO>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllWithIncludesAsync();

            var orderDtos = orders.Select(o => new OrderDTO
            {
                OrderId = o.Id,
                CartId = o.Cart?.Id,
                AccountId = o.AccountId,
                TotalPrice = o.Price,
                Address = o.Address,
                Phone = o.Phone,
                Status = o.Status,
                OrderDate = o.CreatedDate,
                Items = o.Cart?.CartItems.Select(ci => new OrderItemDTO
                {
                    Name = ci.KoiFish?.Name ?? ci.KoiFishy?.Name ?? ci.Consignment?.Name ?? "Unknown", 
                    Price = ci.KoiFish?.Price ?? ci.KoiFishy?.Price ?? ci.Consignment?.Price ?? 0, 

                }).ToList()
            });

            return orderDtos;
        }

        public async Task<Dictionary<int, Dictionary<string, decimal>>> GetMonthlyOrders()
        {
            var orders = await _orderRepository.GetAllAsync();

            var monthlySales = orders
                .Where(order => order.Status == OrderStatus.Paid.ToString())  
                .GroupBy(d => new { d.CreatedDate.Year, d.CreatedDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalPrice = g.Sum(d => d.Price)
                });

            var result = new Dictionary<int, Dictionary<string, decimal>>();

            foreach (var item in monthlySales)
            {
                if (!result.ContainsKey(item.Year))
                {
                    result[item.Year] = new Dictionary<string, decimal>();
                }

                string monthName = new DateTime(item.Year, item.Month, 1).ToString("MMMM");

                result[item.Year][monthName] = item.TotalPrice;
            }

            foreach (var year in result.Keys)
            {
                for (int month = 1; month <= 12; month++)
                {
                    string monthName = new DateTime(year, month, 1).ToString("MMMM");
                    if (!result[year].ContainsKey(monthName))
                    {
                        result[year][monthName] = 0m;  
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
            order.Address = updateOrder.Address;
            order.Phone = updateOrder.Phone;
            order.Status = updateOrder.Status;
            order.Price = updateOrder.Price;
            order.ModifiedDate = DateTime.Now;

            await _orderRepository.UpdateAsync(order);
            return order;
        }
    }
}
