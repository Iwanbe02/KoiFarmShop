using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DTOs.OrderDTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int? CartId { get; set; }
        public int? AccountId { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Status { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDTO>? Items { get; set; }
    }

    public class OrderItemDTO 
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
